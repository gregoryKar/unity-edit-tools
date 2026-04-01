using System.Collections.Generic;
using UnityEngine;

namespace Karianakis.EditTools
{
    internal class ShortcutManager : MonoBehaviour
    {


        static ShortcutManager _instForbidden;
        internal static ShortcutManager _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    _instForbidden = EditSuitFather.GetCanvas().gameObject.AddComponent<ShortcutManager>();
                }

                return _instForbidden;

            }
        }
        List<ShortcutAction> _shortcutList = new();
        [SerializeField] string[] _shortcutDebugArray;



        void Update()
        {
            if (InputManager.GetAnyKeyThisFrame() == false) return;

            foreach (var item in _shortcutList)
            {
                ProcessShortcut(item);
            }
        }


        internal string[] GetAllShortutsInformation()
        {
            _shortcutDebugArray = new string[_shortcutList.Count];
            for (int i = 0; i < _shortcutList.Count; i++)
            {
                _shortcutDebugArray[i] =
                    _shortcutList[i].GetCommandStringId + " = ";

                for (int key = 0; key < _shortcutList[i].GetDualKeys.Length
                ; key++)
                {
                    string extra;
                    string keyString = _shortcutList[i].GetDualKeys[key].GetKeyString();

                    switch(keyString)
                    {
                        case "\0":
                            extra = " NULL";
                            break;
                        case "<":
                            extra = " LEFT-ARROW";
                            break;
                        case ">":
                            extra = " RIGHT-ARROW";
                            break;
                        case "^":
                            extra = " UP-ARROW";
                            break;
                        case "v":
                            extra = " DOWN-ARROW";
                            break;
                        case " ":
                            extra = " SPACE";   
                            break;
                        default:
                            extra = " " + keyString;
                            break;
                    }
                    _shortcutDebugArray[i] += extra;
                }

            }
            return _shortcutDebugArray;

        }


        void ProcessShortcut(ShortcutAction shortcut)
        {
            int keysCount = shortcut.GetDualKeys.Length;
            if (InputManager.GetKeyDown
                (shortcut.GetDualKeys[keysCount - 1]) == false)
                return;


            if (keysCount == 1)
            {
                // last down is enough 
                shortcut.GetAction.Invoke();
            }
            else
            {
                // last down asumed
                // checks all but the last if being held down
                for (int i = 0; i < keysCount - 1; i++)
                {

                    if (InputManager.GetKey(shortcut.GetDualKeys[i]) == false)
                        return;
                }
                shortcut.GetAction.Invoke();
            }









        }


        //? EXPOSED
        internal static void AddShortcut(ShortcutAction shortcut)
        {

            if (_inst._shortcutList.Contains(shortcut)) return;

            foreach (var item in _inst._shortcutList)
            {
                if (item.GetCommandStringId == shortcut.GetCommandStringId)
                {
                    Debug.LogError("Shortcut with command string id " + shortcut.GetCommandStringId + " already exists. Skipping adding this shortcut.");
                    return;
                }
            }

            _inst._shortcutList.Add(shortcut);
        }


    }
}
