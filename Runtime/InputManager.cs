


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
   if (UnityEngine.InputSystem.Keyboard.current != null)
            {
                foreach (var key in UnityEngine.InputSystem.Keyboard.current.allKeys)
                {
                    // key.isPressed
                    if(key.isPressed)
                    {
                        char c = TextUtilities.KeyCodeToChar(key, Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
                           if (c != '\0')
                           {
                            _PressedChars.Add(c);
                            _PressedKeys.Add(key);
                           }

                   
                    }

                    if (key.wasPressedThisFrame)
                    {
                        char c = TextUtilities.KeyCodeToChar(key, Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

                        //is checking whether the character c is not the null character (character code 0).
                           if (c != '\0')
                           {
                         _PressedDownChars.Add(c);
                        _PressedDownKeys.Add(key);
                           }
                    }
                }
            }


#endif
        }

        void UpdateKeysPressedLegacySystem()
        {
#if ENABLE_LEGACY_INPUT_MANAGER

            foreach (char c in Input.inputString)
            {
                _PressedDownChars.Add(c);
                _PressedDownKeys.Add((KeyCode)c);
            }

            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(key)) // held down
                {
                    char c = TextUtilities.KeyCodeToChar(key, Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
                    // You need to implement this mapping
                    if (c != '\0')
                    {
                        _PressedChars.Add(c);
                        _PressedKeys.Add(key);
                    }
                }
            }


#endif
        }







        //? EXPOSED
        public static bool GetKey(KeyCode key)
            =>_inst._PressedKeys.Contains(key);
    
        
        public static bool GetKeyDown(KeyCode key)
            => _inst._PressedDownKeys.Contains(key);
        public static bool GetAnyKey()
            => _inst._PressedKeys.Count > 0;
    



        public static bool GetCharsPressedNow(out char[] charArray)
        {
            charArray = _inst._PressedDownChars.ToArray();
            return charArray.Length > 0;
        }






    }
}