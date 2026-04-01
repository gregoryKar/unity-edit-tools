using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Karianakis.EditTools
{
    internal class CharInputManager : MonoBehaviour
    {


        static CharInputManager _instForbidden;
        internal static CharInputManager _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    _instForbidden = EditSuitFather.GetCanvas().gameObject.AddComponent<CharInputManager>();
                }

                return _instForbidden;

            }
        }



        private readonly Queue<char> _buffer = new Queue<char>();

#if ENABLE_INPUT_SYSTEM
        private System.Action<char> _handler;
#endif

        // Call once (init)
        private void OnEnable()
        {
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null)
            {
                _handler = c => _buffer.Enqueue(c);
                Keyboard.current.onTextInput += _handler;
            }
#endif
        }

        // Call once (cleanup)
        private void OnDisable()
        {
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null && _handler != null)
            {
                Keyboard.current.onTextInput -= _handler;
            }
#endif
        }

        // Call every frame
        public void Update()
        {
#if ENABLE_LEGACY_INPUT_MANAGER
        string input = Input.inputString;

        for (int i = 0; i < input.Length; i++)
        {
            _buffer.Enqueue(input[i]);
        }
#endif
        }

        // Get all chars typed THIS frame
        public List<char> GetCharsThisFrame()
        {
            List<char> result = new List<char>();

            while (_buffer.Count > 0)
            {
                result.Add(_buffer.Dequeue());
            }

            return result;
        }


    }
}