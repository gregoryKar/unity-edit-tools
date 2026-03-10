



using System;
using UnityEngine;


namespace Karianakis.EditTools
{

    public class ShortcutAction
    {
        public string _commandStringId;
        public KeyCode[] _keys;
        public Action _action;
        //public bool _anyKeyIsEnough;




        public static void Create(string command, Action action, params KeyCode[] keys)
        => new ShortcutAction(command, action, keys);//, false


        // public static void CreateOptional(string command, Action action, params KeyCode[] keys)
        // => new ShortcutAction(command, action, keys);//, true






        private ShortcutAction(string command, Action action, params KeyCode[] keys)// bool anyKeyIsEnough
        {

            if (keys == null || keys.Length == 0)
            {
                Debug.LogError("shortcut " + command + " has no keys assigned");
                return;
            }

            bool notPlayMode =
                Application.isEditor && !Application.isPlaying;
            string notInPlayModeWarning = "shortcut " + command + " not in play mode";
#if KARIANAKIS
            if (notPlayMode)
            {
                Debug.LogError(notInPlayModeWarning);
                return;
            }
#else
            if (notPlayMode)
            {
                Debug.LogWarning(notInPlayModeWarning);
                return;
            }
#endif


            this._commandStringId = command;
            this._keys = keys;
            this._action = action;
            //this._anyKeyIsEnough = anyKeyIsEnough;

            ShortcutManager.AddShortcut(this);
        }



    }
}