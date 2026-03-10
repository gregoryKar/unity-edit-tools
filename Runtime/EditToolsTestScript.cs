


using System;
using UnityEngine;


namespace Karianakis.EditTools
{
    class EditToolsTestScript : MonoBehaviour
    {

        //! AIMED FOR MY PERSONAL TESTING AND NOTHING ELSE

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

        void Update()
        {

            // void testKeyCode(KeyCode key)
            // {
            //     if (InputManager.GetKey(key))
            //     {
            //         Debug.LogWarning($"Key {key} pressed");
            //     }
            // }

            // void testKeyCodeDown(KeyCode key)
            // {
            //     if (InputManager.GetKeyDown(key))
            //     {
            //         Debug.LogError($"Key {key} pressed");
            //     }

            // }



            // testKeyCode(KeyCode.Backslash);
            // testKeyCode(KeyCode.Backspace);

            // testKeyCodeDown(KeyCode.Backslash);
            // testKeyCodeDown(KeyCode.Backspace);

            //   testKeyCode(KeyCode.KeypadEnter);
            // testKeyCode(KeyCode.Return);

            // testKeyCodeDown(KeyCode.KeypadEnter);
            // testKeyCodeDown(KeyCode.Return);





        }
    }

}

