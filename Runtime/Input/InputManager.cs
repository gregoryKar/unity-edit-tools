using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Karianakis.EditTools
{
    internal class InputManager
    {


        // static InputManager _instForbidden;
        // static InputManager _inst
        // {
        //     get
        //     {
        //         if (_instForbidden == null)
        //         {
        //             _instForbidden = new InputManager();
        //         }

        //         _instForbidden.UpdateInputLocal();
        //         return _instForbidden;
        //     }
        // }

        // HashSet<KeyCode> _PressedKeys = new();
        // HashSet<KeyCode> _PressedDownKeys = new();
        // HashSet<char> _PressedChars = new();
        // HashSet<char> _PressedDownChars = new();


        int _lastUpdateFrame = -10;

        //         void UpdateInputLocal()
        //         {

        //             if (_lastUpdateFrame == Time.frameCount) return;
        //             _lastUpdateFrame = Time.frameCount;


        //             _PressedKeys.Clear();
        //             _PressedDownKeys.Clear();
        //             _PressedChars.Clear();
        //             _PressedDownChars.Clear();



        // #if ENABLE_LEGACY_INPUT_MANAGER
        //             UpdateKeysPressedLegacySystem();
        // #endif
        //             // New Input System
        // #if ENABLE_INPUT_SYSTEM
        //             UpdateKeysPressedNewInputSystem();
        // #endif 



        //         }



        public static bool GetKey(MyKey key)
            => GetKeyFunction(key, false);
        public static bool GetKeyDown(MyKey key)
            => GetKeyFunction(key, true);
        static bool GetKeyFunction(MyKey key, bool down)
        {
#if ENABLE_INPUT_SYSTEM
            if (UnityEngine.InputSystem.Keyboard.current == null)
            {
                return false;
            }
            var control = UnityEngine.InputSystem.Keyboard.current[key._newKey];
            if (control == null)
            {
                Debug.LogWarning($"Control for key {key._newKey} is null");
                return false;
            }


            if (down)
            {
                if (control.wasPressedThisFrame)
                {
                    return true;
                }
            }
            else if (control.isPressed)
            {
                return true;
            }


#endif

#if ENABLE_LEGACY_INPUT_MANAGER
        if(down)
        {
            if (Input.GetKeyDown(key.oldKey))
                return true;
        }
        else if (Input.GetKey(key.oldKey))
            return true;
        }
#endif

            return false;
        }

        public static bool GetAnyKeyThisFrame()
        {
#if ENABLE_INPUT_SYSTEM
            if (UnityEngine.InputSystem.Keyboard.current != null &&

             ( UnityEngine.InputSystem.Keyboard.current.anyKey.wasPressedThisFrame 
             || UnityEngine.InputSystem.Keyboard.current.anyKey.isPressed))
                return true;
#endif

#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.anyKeyDown || Input.anyKey)
            return true;
#endif

            return false;
        }




        // private void OnEnable()
        // {
        //     Keyboard.current.onTextInput += c => buffer.Enqueue(c);
        // }

        // private void OnDisable()
        // {
        //     Keyboard.current.onTextInput -= c => buffer.Enqueue(c); // ⚠ not ideal (see note below)
        // }

        // void Update()
        // {
        //     while (buffer.Count > 0)
        //     {
        //         char c = buffer.Dequeue();
        //         Debug.Log("Frame processed char: " + c);
        //     }
        // }

        //         void UpdateKeysPressedNewInputSystem()
        //         {
        // #if ENABLE_INPUT_SYSTEM 
        //             if (UnityEngine.InputSystem.Keyboard.current == null) return;
        //             if (UnityEngine.InputSystem.Keyboard.current.anyKey.isPressed == false)
        //             {
        //                 return;
        //             }


        //             bool shiftPressed = UnityEngine.InputSystem.Keyboard.current.shiftKey.isPressed;
        //             bool shiftPressedLeft = UnityEngine.InputSystem.Keyboard.current.leftShiftKey.isPressed;
        //             bool shiftPressedRight = UnityEngine.InputSystem.Keyboard.current.rightShiftKey.isPressed;

        //             bool anyShiftKeyPressed = shiftPressed || shiftPressedLeft || shiftPressedRight;




        //             foreach (var key in UnityEngine.InputSystem.Keyboard.current.allKeys)
        //             {


        //                 if (key == null) { Debug.LogWarning($"Key is null"); continue; }
        //                 else
        //                 {
        //                     Debug.Log($"Key is NOT null {key.name}");
        //                 }

        //                 KeyCode keyCode = (KeyCode)key.keyCode;
        //                 if (key.wasPressedThisFrame)
        //                 {
        //                     Debug.LogError($"Key wasPressedThisFrame {key.name}");
        //                     _PressedDownKeys.Add(keyCode);
        //                     char c = TextUtilities.KeyCodeToChar
        //                         (keyCode, anyShiftKeyPressed);
        //                     //is checking whether the character c is not the nullcharacter (character code 0).
        //                     Debug.LogError($"CHAR wasPressedThisFrame  {c}");
        //                     if (c != '\0')
        //                     {
        //                         Debug.LogError($"Key PASSED wasPressedThisFrame {key.name}");
        //                         _PressedDownChars.Add(c);
        //                     }
        //                 }
        //                 // key.isPressed
        //                 else if (key.isPressed)
        //                 {
        //                     Debug.LogError($"Key isPressed {key.name}");
        //                     _PressedKeys.Add(keyCode);
        //                     char c = TextUtilities.KeyCodeToChar(keyCode, anyShiftKeyPressed);

        //                     Debug.LogError($"CHAR isPressed  {c}");

        //                     if (c != '\0')
        //                     {
        //                         Debug.LogError($"Key PASSED isPressed {key.name}");
        //                         _PressedChars.Add(c);

        //                     }
        //                 }


        //             }


        // #endif
        //         }

        //         void UpdateKeysPressedLegacySystem()
        //         {
        // #if ENABLE_LEGACY_INPUT_MANAGER

        //             // for keys that don't have a char representation, we can only detect them via KeyCode
        //             // foreach (char c in Input.inputString)
        //             // {
        //             //     _PressedDownChars.Add(c);
        //             //     _PressedDownKeys.Add((KeyCode)c);
        //             // }


        //             bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        //             foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        //             {

        //                 if (Input.GetKeyDown(key))
        //                 {
        //                     _PressedDownKeys.Add(key);
        //                     char c = TextUtilities.KeyCodeToChar(key, shiftPressed);
        //                     // You need to implement this mapping
        //                     if (c != '\0')
        //                     {
        //                         _PressedDownChars.Add(c);
        //                     }

        //                 }
        //                 else if (Input.GetKey(key)) // held down
        //                 {

        //                     _PressedKeys.Add(key);
        //                     char c = TextUtilities.KeyCodeToChar(key, shiftPressed);
        //                     // You need to implement this mapping
        //                     if (c != '\0')
        //                     {
        //                         _PressedChars.Add(c);
        //                     }
        //                 }
        //             }


        // #endif
        //         }







        //         //? EXPOSED
        //         public static bool GetKey(KeyCode key)
        //             => _inst._PressedKeys.Contains(key);


        //         public static bool GetKeyDown(KeyCode key)
        //             => _inst._PressedDownKeys.Contains(key);
        //         public static bool GetAnyKeyAnyState()
        //             => _inst._PressedKeys.Count > 0 ||
        //             _inst._PressedDownKeys.Count > 0;




        //         public static bool GetCharsPressedNow(out char[] charArray)
        //         {
        //             charArray = _inst._PressedDownChars.ToArray();
        //             return charArray.Length > 0;
        //         }






    }
}