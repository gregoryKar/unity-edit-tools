


using UnityEngine;


namespace Karianakis.EditTools
{
    class EditToolsTestScript : MonoBehaviour
    {

        [SerializeField] TMPro.TextMeshProUGUI _textDown;
        [SerializeField] TMPro.TextMeshProUGUI _text;


        void Start()
        {
            var s = EditToolsSettings.GetOrCreateSettings();
            Debug.LogError(s == null ? "null" : "not null");
        }

        void Update()
        {
            if (InputManager.GetCharsPressedNow(out char[] chars))
            {
                //Debug.Log("Pressed chars: " + string.Join(", ", chars));
                if (_textDown != null) _textDown.text = "Pressed chars: " + string.Join("", chars);
                if (_text != null) _text.text += string.Join("", chars);
            }
        }
    }

}