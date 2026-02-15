

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Karianakis.EditTools
{

    public class ShortcutManager : MonoBehaviour
    {






        static ShortcutManager _instForbidden;
        public static ShortcutManager _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    var obj = new GameObject("ShortcutManager");

                    _instForbidden = obj.AddComponent<ShortcutManager>();
                    obj.transform.SetParent(EditSuitFather._inst.transform, true);
                }

                return _instForbidden;

            }
            set { _instForbidden = value; }



        }



        void Update()
        {
            CurrentPressedKeys.Clear();
            CurrentPressedDownKeys.Clear();

            if (Input.anyKey is false) return;


            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(key))
                {
                    CurrentPressedKeys.Add(key);

                    if (Input.GetKeyDown(key))
                        CurrentPressedDownKeys.Add(key);

                }
            }

            foreach (var item in _shortcutList)
            {
                ProcessShortcut(item);
            }



        }

        HashSet<KeyCode> CurrentPressedKeys = new HashSet<KeyCode>();
        HashSet<KeyCode> CurrentPressedDownKeys = new HashSet<KeyCode>();

        List<ShortcutAction> _shortcutList = new();

        [SerializeField] string[] _shortcutDebugArray;
        void DebbugShortcuts()
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



        public static void AddShortcut(ShortcutAction shortcut)
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
            _inst.DebbugShortcuts();

        }
        void ProcessShortcut(ShortcutAction shortcut)
        {

            /* if multiple shortcuts you dont want only on down .. ..
             if single looks on down , if multiple ?? 
             maybe the last one must be on down .. 
            */

            bool allKeys = true;
            bool anyKeyDown = false;


            foreach (var item in shortcut._keys)
            {
                if (CurrentPressedKeys.Contains(item) is false)
                    allKeys = false;

                if (CurrentPressedDownKeys.Contains(item)) anyKeyDown = true;
            }

            bool lastKeyDown = CurrentPressedDownKeys.Contains(shortcut._keys[shortcut._keys.Length - 1]);




            if (anyKeyDown is false) return;
            if (shortcut._anyKeyIsEnough is false)
            {
                if (lastKeyDown is false) return;
                if (allKeys is false) return;
            }



            shortcut._action.Invoke();


        }



    }

}
