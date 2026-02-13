



using UnityEngine;

namespace Karianakis.EditTools
{

    public class EditSuitFather : MonoBehaviour
    {

        static EditSuitFather _instForbidden;
        public static EditSuitFather _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    var obj = new GameObject("EDIT-SUIT-FATHER");
                    var rect = obj.AddComponent<RectTransform>();
                    rect.anchoredPosition = Vector2.zero;

                    _instForbidden = obj.AddComponent<EditSuitFather>();
                    _instForbidden.FindAndSetParentCanvas();

                }

                return _instForbidden;

            }
            set { _instForbidden = value; }



        }





        RectTransform _canvas;
        public RectTransform GetCanvas() => _canvas;
        void FindAndSetParentCanvas()
        {

            _canvas = null;
            foreach (var canvasItem in FindObjectsByType<Canvas>(FindObjectsSortMode.InstanceID))
            {
                if (canvasItem.renderMode == RenderMode.WorldSpace)
                    continue;

                _canvas = canvasItem.GetComponent<RectTransform>();
                break;

            }

            if (_canvas == null) Debug.LogError("NULL CANVAS FOUND FOR EDITSUITFATHER");
            else transform.SetParent(_canvas.transform, false);

        }



        // 0,0 : bottom left , 1,1 : top right
        public void SetPosRelativeToScreen(RectTransform rect, Vector2 normalizedScreenPos)
        {
            Vector2 screenPos = new Vector2(
                normalizedScreenPos.x * Camera.main.pixelWidth,
                normalizedScreenPos.y * Camera.main.pixelHeight
            );

            Vector3 posVek3 = screenPos;
            posVek3.z = rect.position.z;

            Debug.LogError(posVek3);

            rect.position = posVek3;
        }


    }
}