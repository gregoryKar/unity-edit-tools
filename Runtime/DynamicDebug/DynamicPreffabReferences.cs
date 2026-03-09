
using UnityEngine;


namespace Karianakis.EditTools
{
    internal class DynamicPreffabReferences : MonoBehaviour
    {



        [SerializeField] GameObject _templateText;
        [SerializeField] GameObject _extrasToggle;
        [SerializeField] GameObject _extrasEnabledIcon;
        [SerializeField] GameObject _sizeIncreaseButton;
        [SerializeField] GameObject _sizeDecreaseButton;
        [SerializeField] GameObject _shortcutHintText;
        [SerializeField] GameObject _nextPageButton;
        [SerializeField] GameObject _previousPageButton;
        [SerializeField] GameObject _pageIndexText;
        [SerializeField] GameObject _settingsButton;



        public GameObject GetTemplateText => _templateText;
        public GameObject GetExtrasToggle => _extrasToggle;
        public GameObject GetExtrasEnabledIcon => _extrasEnabledIcon;
        public GameObject GetSizeIncreaseButton => _sizeIncreaseButton;
        public GameObject GetSizeDecreaseButton => _sizeDecreaseButton;
        public GameObject GetShortcutHintText => _shortcutHintText;

        public GameObject GetNextPageButton => _nextPageButton;
        public GameObject GetPreviousPageButton => _previousPageButton;
        public GameObject GetPageIndexText => _pageIndexText;
        public GameObject GetSettingsButton => _settingsButton;


    }
}