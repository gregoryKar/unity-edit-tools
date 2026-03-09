

using UnityEngine;

namespace Karianakis.EditTools
{
    internal class CommandTerminalReferences : MonoBehaviour
    {

        [SerializeField] TMPro.TextMeshProUGUI _inputDisplayText;
        [SerializeField] TMPro.TextMeshProUGUI _suggestionsText;
        [SerializeField] TMPro.TextMeshProUGUI _logsAboveText;
        [SerializeField] TMPro.TextMeshProUGUI _toggleShortcutText;
        [SerializeField] TMPro.TextMeshProUGUI _paramsShowcaseText;
        
        [SerializeField]GameObject _sizeIncreaseButton;
        [SerializeField]GameObject _sizeDecreaseButton; 
        [SerializeField]GameObject _settingsButton; 



        public TMPro.TextMeshProUGUI GetInputDisplayText => _inputDisplayText;
        public TMPro.TextMeshProUGUI GetSuggestionsText => _suggestionsText;
        public TMPro.TextMeshProUGUI GetLogsAboveText => _logsAboveText;
        public TMPro.TextMeshProUGUI GetToggleShortcutText => _toggleShortcutText;
        public TMPro.TextMeshProUGUI GetParamsShowcaseText => _paramsShowcaseText;

        public GameObject GetSizeIncreaseButton => _sizeIncreaseButton;
        public GameObject GetSizeDecreaseButton => _sizeDecreaseButton;
        public GameObject GetSettingsButton => _settingsButton;



    }
}