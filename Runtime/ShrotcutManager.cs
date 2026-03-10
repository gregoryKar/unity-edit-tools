

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Karianakis.EditTools
{

    internal class ShortcutManager : MonoBehaviour
    {


        static ShortcutManager _instForbidden;
        public static ShortcutManager _inst
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



        void Start()
        {
            ShortcutAction.Create("test 01", () =>
            {
              
            }, KeyCode.Space, KeyCode.T);
        }

        void Update()
        {

            if (InputManager.GetAnyKey() == false) return;

            foreach (var item in _shortcutList)
            {
                ProcessShortcut(item);
            }

        }


        void DebugShortcuts()
        {

            _shortcutDebugArray = new string[_shortcutList.Count];
            for (int i = 0; i < _shortcutList.Count; i++)
            {
                _shortcutDebugArray[i] = _shortcutList[i]._commandStringId;

                _shortcutDebugArray[i] += " :: ";
                foreach (var item in _shortcutList[i]._keys)
                {
                    _shortcutDebugArray[i] += "-" + item.ToString();
                }
            }
        }
        void ProcessShortcut(ShortcutAction shortcut)
        {

            bool allKeys = true;



            foreach (var item in shortcut._keys)
            {
                if (InputManager.GetKey(item) == false)
                    allKeys = false;
            }

            bool lastKeyDown = InputManager.GetKeyDown(shortcut._keys[shortcut._keys.Length - 1]);


            if (allKeys is false) return;
            if (lastKeyDown is false) return;


            shortcut._action.Invoke();

        }


        //? EXPOSED
        internal static void AddShortcut(ShortcutAction shortcut)
        {

            if (_inst._shortcutList.Contains(shortcut)) return;

            foreach (var item in _inst._shortcutList)
            {
                if (item._commandStringId == shortcut._commandStringId)
                {
                    Debug.LogError("Shortcut with command string id " + shortcut._commandStringId + " already exists. Skipping adding this shortcut.");
                    return;
                }
            }

            _inst._shortcutList.Add(shortcut);
            _inst.DebugShortcuts();

        }



    }
}
