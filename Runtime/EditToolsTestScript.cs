


using System;
using UnityEngine;


namespace Karianakis.EditTools
{
    class EditToolsTestScript : MonoBehaviour
    {

        void Start()
        {
            ShortcutAction.Create("simple shortcut with space and A", () => Debug.Log("SPACE A SIMPLE"), KeyCode.Space, KeyCode.A);

            ShortcutAction.Create("simple shortcut with JUST A", () => Debug.Log("JUST A SIMPLE"), KeyCode.A);


            CustomCommand.Simple("simple command", () => Debug.Log("SIMPLE COMMAND EXECUTED"));

            CustomCommand.OneParam<int>("one param command", (a) => Debug.Log($"ONE PARAM COMMAND EXECUTED with param {a}"));

            CustomCommand.FromDelegate("from delegate"
            , new Action<int, float, string>
            ((a, b, c) => Debug.Log($"{a}{b}{c}")));


        }

    }

}