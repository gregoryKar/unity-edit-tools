



using System;
using UnityEngine;


namespace Karianakis.EditTools
{

    public class ShortcutAction
    {
        public string _commandStringId;
        public KeyCode[] _keys;
        public Action _action;
        public bool _anyKeyIsEnough;




        public static void Create(string command, Action action, params KeyCode[] keys)
        => new ShortcutAction(command, action, false, keys);


        public static void CreateOptional(string command, Action action, params KeyCode[] keys)
        => new ShortcutAction(command, action, true, keys);






        private ShortcutAction(string command, Action action, bool anyKeyIsEnough, params KeyCode[] keys)
        {


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
            this._anyKeyIsEnough = anyKeyIsEnough;

            ShortcutManager.AddShortcut(this);
        }



    }
}