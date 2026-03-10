


using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Karianakis.EditTools
{
    internal class InputManager
    {


        static InputManager _instForbidden;
        static InputManager _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    _instForbidden = new InputManager();
                }

                _instForbidden.UpdateInputLocal();
                return _instForbidden;
            }
        }



        HashSet<KeyCode> _PressedKeys = new();
        HashSet<KeyCode> _PressedDownKeys = new();
        HashSet<char> _PressedChars = new();
        HashSet<char> _PressedDownChars = new();


        int _lastUpdateFrame = -10;

        void UpdateInputLocal()
        {

            if (_lastUpdateFrame == Time.frameCount) return;
            _lastUpdateFrame = Time.frameCount;


            _PressedKeys.Clear();
            _PressedDownKeys.Clear();
            _PressedChars.Clear();
            _PressedDownChars.Clear();



#if ENABLE_LEGACY_INPUT_MANAGER
            UpdateKeysPressedLegacySystem();
#endif
            // New Input System
#if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_PACKAGE
           UpdateKeysPressedNewInputSystem();
#endif

        }



        void UpdateKeysPressedNewInputSystem()
        {
#if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_PACKAGE
   if (UnityEngine.InputSystem.Keyboard.current == null)return;
            
            bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || InputGetKey(KeyCode.RightShift);
            
            foreach (var key in UnityEngine.InputSystem.Keyboard.currentallKeys)
            {

                if (key.wasPressedThisFrame)
                {
                    _PressedDownKeys.Add(key);
                    char c = TextUtilities.KeyCodeToChar(key, shiftPressed);
                    //is checking whether the character c is not the nullcharacter (character code 0).
                    if (c != '\0')
                    {
                    _PressedDownChars.Add(c);
                    }
                }
                // key.isPressed
                else if(key.isPressed)
                {
                    _PressedKeys.Add(key);
                    char c = TextUtilities.KeyCodeToChar(key, shiftPressed);
                    if (c != '\0')
                    {
                     _PressedChars.Add(c);
                    
                    }
                }
                
                
            }


#endif
        }

        void UpdateKeysPressedLegacySystem()
        {
#if ENABLE_LEGACY_INPUT_MANAGER

            // for keys that don't have a char representation, we can only detect them via KeyCode
            // foreach (char c in Input.inputString)
            // {
            //     _PressedDownChars.Add(c);
            //     _PressedDownKeys.Add((KeyCode)c);
            // }

            bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {

                if (Input.GetKeyDown(key))
                {
                    _PressedDownKeys.Add(key);
                    char c = TextUtilities.KeyCodeToChar(key, shiftPressed);
                    // You need to implement this mapping
                    if (c != '\0')
                    {
                        _PressedDownChars.Add(c);
                    }

                }
                else if (Input.GetKey(key)) // held down
                {

                    _PressedKeys.Add(key);
                    char c = TextUtilities.KeyCodeToChar(key, shiftPressed);
                    // You need to implement this mapping
                    if (c != '\0')
                    {
                        _PressedChars.Add(c);
                    }
                }
            }


#endif
        }







        //? EXPOSED
        public static bool GetKey(KeyCode key)
            => _inst._PressedKeys.Contains(key);


        public static bool GetKeyDown(KeyCode key)
            => _inst._PressedDownKeys.Contains(key);
        public static bool GetAnyKeyAnyState()
            => _inst._PressedKeys.Count > 0 ||
            _inst._PressedDownKeys.Count > 0;




        public static bool GetCharsPressedNow(out char[] charArray)
        {
            charArray = _inst._PressedDownChars.ToArray();
            return charArray.Length > 0;
        }






    }
}