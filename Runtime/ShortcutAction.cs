



using System;
using UnityEngine;


namespace Karianakis.EditTools
{

    public class ShortcutAction
    {
        string _commandStringId;
        Action _action;

        internal string GetCommandStringId => _commandStringId;
        internal Action GetAction => _action;

        MyKey[] _dualKeys;
        internal MyKey[] GetDualKeys => _dualKeys;


        public static void Create(string command, Action action, params KeyCode[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                Debug.LogError("shortcut " + command + " has no keysassigned");
                return;
            }
            var shortcut = new ShortcutAction(command, action);

            shortcut._dualKeys = new MyKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                shortcut._dualKeys[i] = new MyKey(keys[i]);
            }

        }

        private ShortcutAction(string command, Action action)
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


            _commandStringId = command;
            _action = action;

            ShortcutManager.AddShortcut(this);
        }

    }
}