



using System;
using UnityEngine;

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
        this._commandStringId = command;
        this._keys = keys;
        this._action = action;
        this._anyKeyIsEnough = anyKeyIsEnough;

        ShortcutManager.AddShortcut(this);
    }



}