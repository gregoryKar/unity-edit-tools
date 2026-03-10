



using Karianakis.EditTools;
using UnityEngine;

namespace Karianakis.EditTools
{

    public class EditToolsShowcase : MonoBehaviour
    {


        void Start()
        {

            DynamicDebug.Create("simple")
                .SetContent("this will apear on the panel");
            // you can 

            DynamicDebug.Create("dislay value that changes over time")
                .SetColor(Color.cyan)//adds custom color
                .SetUpdate(1f)// updates the value reapeated
                .SetDynamicContent(() => "Dynamic content at " + Time.time.
            ToString("F2"));//gets the new value every time it gets updated





        }

        void Update()
        {
         
        }

    }
}