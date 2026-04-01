


using System;
using UnityEngine;


namespace Karianakis.EditTools
{
    class TestUnityEditTools : MonoBehaviour
    {


        [DebugVariable("code", nickname: "test var")]
        float _testVariable;

        [DebugVariable("code2", nickname: "test var two")]
        float _testVariableTwo;


        //! AIMED FOR MY PERSONAL TESTING AND NOTHING ELSE
        int _testInt;

        [SerializeField] bool _highlight;
        [SerializeField] bool _highlightError;
        [SerializeField] bool _highlightGreen;

        void Start()
        {

            if (_highlight)
            {
                if (_highlightError)
                {
                    StyledHierarchyItem.HighlightError(gameObject);
                }
                else if (_highlightGreen)
                {
                    StyledHierarchyItem.HighlightGreen(gameObject);
                }
            }

            DynamicDebug.Create("testCode").SetContent("say this");



            // ShortcutAction.Create("simple shortcut with space and A", () => Debug.Log("SPACE A SIMPLE"), KeyCode.Space, KeyCode.A);

            // ShortcutAction.Create("simple shortcut with JUST A", () => Debug.Log("JUST A SIMPLE"), KeyCode.A);


            CustomCommand.Simple("simple command", () => Debug.Log("SIMPLE COMMAND EXECUTED"));

            CustomCommand.OneParam<int>("one param command", (a) => Debug.Log($"ONE PARAM COMMAND EXECUTED with param {a}"));

            CustomCommand.FromDelegate("from delegate"
            , new Action<int, float, string>
            ((a, b, c) => Debug.Log($"{a}{b}{c}")));


        }

        void Update()
        {
            _testInt++;



            DynamicDebug.Create("testCode").SetContent("say this updated" + _testInt);
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



        [ConsoleCommand("nicknameoftestFunction")]
        void testFunction() { Debug.LogError("testFunction"); }

        [ConsoleCommand(nickname: "nicknameoftestFunctionTwo")]
        void testFunctionTwo() { Debug.LogError("testFunctionTwo"); }

        [ConsoleCommand()]
        void testFunctionThree()
        {
            Debug.LogError("testFunctionThree");
        }

        [ConsoleCommand()]
        void TestParamInt(int a) { Debug.LogError(a); }
        [ConsoleCommand()] void TestParamFloat(float a) { Debug.LogError(a); }
        [ConsoleCommand()] void TestParamString(string a) { Debug.LogError(a); }
        [ConsoleCommand()] void TestParamTwo(string a, int b) { Debug.LogError(a + "_" + b); }
        [ConsoleCommand()] void TestParamThree(string a, int b, float c) { Debug.LogError(a + "_" + b + "_" + c); }
    }

}

