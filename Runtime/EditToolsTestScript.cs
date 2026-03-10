


using UnityEngine;


namespace Karianakis.EditTools
{
    class EditToolsTestScript : MonoBehaviour
    {

        void Start()
        {
            ShortcutAction.Create("simple shortcut with space and A", () => Debug.Log("SPACE A SIMPLE"), KeyCode.Space, KeyCode.A);

        }

    }

}