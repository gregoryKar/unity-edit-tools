using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
namespace Karianakis.EditTools
{
    internal struct MyKey
    {

        internal KeyCode _oldKey;
#if ENABLE_INPUT_SYSTEM
        internal Key _newKey;
#endif

        internal string GetKeyString()
            => TextUtilities.KeyCodeToChar(_oldKey, false).ToString();



        internal MyKey(KeyCode oldKey)
        {
            _oldKey = oldKey;
#if ENABLE_INPUT_SYSTEM
            _newKey = KeycodeToNewKey(_oldKey);
#endif

        }


#if ENABLE_INPUT_SYSTEM
        public static Key KeycodeToNewKey(KeyCode keyCode)
        {
            // Letters
            if (keyCode >= KeyCode.A && keyCode <= KeyCode.Z)
            {
                return (Key)((int)Key.A + (keyCode - KeyCode.A));
            }

            // Numbers (top row)
            if (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9)
            {
                return (Key)((int)Key.Digit0 + (keyCode - KeyCode.Alpha0));
            }


            // Add more as needed...

            // extras
            switch (keyCode)
            {
                case KeyCode.Space:
                    return Key.Space;

                case KeyCode.Return:
                case KeyCode.KeypadEnter:
                    return Key.Enter;

                case KeyCode.Backspace:
                    return Key.Backspace;

                case KeyCode.Tab:
                    return Key.Tab;

                case KeyCode.Escape:
                    return Key.Escape;


                case KeyCode.LeftShift:
                    return Key.LeftShift;

                case KeyCode.RightShift:
                    return Key.RightShift;

                case KeyCode.UpArrow:
                    return Key.UpArrow;

                case KeyCode.DownArrow:
                    return Key.DownArrow;

                case KeyCode.LeftArrow:
                    return Key.LeftArrow;

                case KeyCode.RightArrow:
                    return Key.RightArrow;
            }

            return Key.None; // fallback

        }
#endif

    }
}