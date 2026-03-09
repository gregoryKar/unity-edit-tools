using UnityEngine;
using UnityEngine.UI;

namespace Karianakis.EditTools
{

    internal class EditSuitFather : MonoBehaviour
    {

        static EditSuitFather _instForbidden;
        static EditSuitFather _inst
        {
            get
            {

                if (_instForbidden == null)
                {

                    var obj = CreateAndConfigureVanvas();
                    obj.name = "EDIT-SUIT-CANVAS";

                    _instForbidden = obj.gameObject
                        .AddComponent<EditSuitFather>();
                    _instForbidden._canvasRect = obj;

                }

                return _instForbidden;

            }

        }


        RectTransform _canvasRect;


        static RectTransform CreateAndConfigureVanvas()
        {
            GameObject obj = new GameObject("Karianakis Canvas",
            typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
           
            DontDestroyOnLoad(obj);

            var canvas = obj.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 69;

            var scaler = obj.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = 0.5f;

            // var raycaster = obj.GetComponent<GraphicRaycaster>();
            // raycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

            return obj.GetComponent<RectTransform>();

        }

       



        //? EXPOSED
        //? SINGLE ENTRY POINT FORBIDDEN IN NOT PLAY MODE
        public static RectTransform GetCanvas()
        {
            if (Application.isPlaying == false)
            {
                Debug.LogError("CALLED EDIT SUIT FATHER IN EDIT MODE, THIS IS NOT ALLOWED");
                return null;
            }

            return _inst._canvasRect;
        }




    }
}