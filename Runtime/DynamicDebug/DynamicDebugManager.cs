using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Karianakis.Utilities;


namespace Karianakis.EditTools
{
    public class DynamicDebugManager : MonoBehaviour
    {

        static DynamicDebugManager _instForbidden;
        static DynamicDebugManager _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    var obj = SpawnMyself();
                    obj.name = "DynamicDebugManager";

                    _instForbidden = obj.AddComponent<DynamicDebugManager>();
                    obj.transform.SetParent(EditSuitFather.GetCanvas(), false);
                    // false i think irelevant you configure position in Configure rect below

                    _instForbidden.ConfigureRect(obj);
                    _instForbidden.ConfigureAllTheRest(obj);
                    _instForbidden.WirePageObjects(obj);

                }

                return _instForbidden;

            }

        }



        //? Constants
        const string _preffabCode = "DebugITemsFatherLayout";
        const string _visibilityKey = "DynamicDebugVisibility";
        const float _sizeIncreaseAmount = 0.2f;
        readonly Color _shortcutHintColor = new Color(0.8f, 0.8f, 0.8f);

        //? EHH ??
        float _referenceDeltaSizeX;
        KeyCode[] _toggleKeys = new KeyCode[] { KeyCode.Space, KeyCode.W };
        GameObject _kidContainerObj;
        TMPro.TextMeshProUGUI _pageIndexText;
        RectTransform _containerRect;
        [SerializeField] Image _extrasEnabledIcon;
        [SerializeField] TMPro.TextMeshProUGUI _tmpTemplate;

        //? VARIABLES
        private Dictionary<string, DynamicDebugItem> _debugItems = new();
        [SerializeField] bool _allExtrasVissibility = false;
        [SerializeField] int _pageIndex = 0;

        bool GetPanelVissible => gameObject.activeSelf;
        int GetItemsCount => _debugItems.Count;

        int GetItemsDisplayCount =>
            EditToolsSettings.GetDynamicDebugPageDisplayCount;


        //? START 
        void Start()
        {

            ShortcutAction.Create("EditDebuggerToggle", () => ReverseVissibility(), _toggleKeys);

            StartSetVissibility();

            RefreshPageLocal();

            //     EditTerminal.AddCommand("refreshPage", () =>
            //     {
            //         RefreshPage();
            //     });
            //     EditTerminal.AddCommand("nextPage", () =>
            //    {
            //        NextPage();
            //    });
            //     EditTerminal.AddCommand("previousPage", () =>
            //    {
            //        PreviousPage();
            //    });
        }



        //? FUNCTIONS
        static GameObject SpawnMyself()
            => Instantiate(Resources.Load<GameObject>(_preffabCode));

        void ConfigureRect(GameObject obj)
        {

            _kidContainerObj = obj.transform.GetChild(0).gameObject;
            _containerRect = obj.GetComponent<RectTransform>();

            RectTransform rect = obj.GetComponent<RectTransform>();

            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 1);

            rect.offsetMin = new Vector2(rect.offsetMin.x, -6);
            rect.offsetMax = new Vector2(rect.offsetMax.x, 6);

            Vector2 anchorePos = rect.anchoredPosition;
            anchorePos.x = 6;
            rect.anchoredPosition = anchorePos;

            Vector2 sizeDelta = rect.sizeDelta;
            sizeDelta.x = 206;
            rect.sizeDelta = sizeDelta;


            _referenceDeltaSizeX = rect.sizeDelta.x;

            float size = EditToolsSettings.GetDynamicDebugSize;
            SetTransformSize(size);

        }
        void ConfigureAllTheRest(GameObject obj)
        {

            RectTransform rect = obj.GetComponent<RectTransform>();

            DynamicPreffabReferences references =
                rect.gameObject.GetComponent<DynamicPreffabReferences>();


            _tmpTemplate = references.GetTemplateText.GetComponent
                <TMPro.TextMeshProUGUI>();
            _tmpTemplate.gameObject.SetActive(false);

            _extrasEnabledIcon = references.GetExtrasEnabledIcon
                .GetComponent<Image>();

            references.GetExtrasToggle.GetComponent<Button>().onClick
                .AddListener(() => ToggleTextsExtrasVissibility());


            references.GetSizeIncreaseButton.GetComponent<Button>().onClick
                         .AddListener(() => ChangeTransformSize(_sizeIncreaseAmount));
            references.GetSizeDecreaseButton.GetComponent<Button>().onClick
                         .AddListener(() => ChangeTransformSize(-_sizeIncreaseAmount));


            var shortcutHintTmp = references.GetShortcutHintText.GetComponent<TMPro.TextMeshProUGUI>();
            shortcutHintTmp.text = "Toggle : " + string.Join(" + ", _toggleKeys);
            shortcutHintTmp.color = _shortcutHintColor;




        }
        void WirePageObjects(GameObject obj)
        {

            DynamicPreffabReferences references =
                            obj.GetComponent<DynamicPreffabReferences>();

            references.GetNextPageButton.GetComponent<Button>().onClick
                .AddListener(() => NextPage());

            references.GetPreviousPageButton.GetComponent<Button>().onClick
                .AddListener(() => PreviousPage());

            //OpenSettingsPanel
            references.GetSettingsButton.GetComponent<Button>().onClick
                .AddListener(() => EditToolsSettingsProvider.OpenSettingsPanel());

            _pageIndexText = references.GetPageIndexText
                .GetComponent<TMPro.TextMeshProUGUI>();

        }



        void ChangeTransformSize(float change)
        {
            float valueBefore = _kidContainerObj.transform.localScale.x;
            float newValue = valueBefore + change;

            newValue = EditToolsSettings.ClampDynamicDebugSize(newValue);
            EditToolsSettings.SetDynamicDebugSize(newValue);

            SetTransformSize(newValue);

        }
        void SetTransformSize(float size)
        {
            _kidContainerObj.transform
                .localScale = Vector3.one * size;

            float newSizeDeltaX = _referenceDeltaSizeX * size;

            Vector2 sizeDelta = _containerRect.sizeDelta;
            sizeDelta.x = newSizeDeltaX;
            _containerRect.sizeDelta = sizeDelta;

        }


        void ToggleTextsExtrasVissibility()
        {
            _allExtrasVissibility = !_allExtrasVissibility;
            //_extrasEnabledIcon.gameObject.SetActive(_allExtrasVissibility);
            _extrasEnabledIcon.color = _allExtrasVissibility ? Color.green
                 : Color.red;

            RefreshItems();

        }



        bool GetStartVisibility()
        {

            if (PlayerPrefs.HasKey(_visibilityKey))
            {
                return PlayerPrefs.GetInt(_visibilityKey) == 1;
            }
            else
            {
                PlayerPrefs.SetInt(_visibilityKey, 0);
                return false;
            }
        }
        void StartSetVissibility()
        {

            bool startVissibility = GetStartVisibility();

            SetVissiblity(startVissibility);


            // check and open / close +
            // set the visibility last visibility acordingly
            // now need double tap tp change id start open
        }
     
        void ReverseVissibility()
        {
            bool currentVissibility = GetPanelVissible;
            currentVissibility = !currentVissibility;
            SetVissiblity(currentVissibility);

        }
        void SetVissiblity(bool value)
        {
            gameObject.SetActive(value);
            PlayerPrefs.SetInt(_visibilityKey, value ? 1 : 0);
        }


        void ReorderDebuggItemsDepricated()
        {
            return;
            foreach (var dictionaryItem in _debugItems)
            {
                var item = dictionaryItem.Value;
                if (item.GetPinTop)
                    item.GetTmp.transform.SetAsFirstSibling();
                else if (item.GetPinBottom)
                    item.GetTmp.transform.SetAsLastSibling();
            }
        }
        void RefreshItems()
        {
            foreach (var item in _debugItems)
            {
                item.Value.Refresh();
            }
        }



        //? PAGE HANDLING

        void NextPage()
            => ChangePage(1);
        void PreviousPage()
            => ChangePage(-1);
        void ChangePage(int direction)
        {

            if (direction != 1 &&
            direction != -1) return;

            int newPage = _pageIndex + direction;
            int maxPage = (GetItemsCount + 1) / GetItemsDisplayCount;

            if (newPage < 0)
                newPage = 0;
            else if (newPage > maxPage)
                newPage = maxPage;

            _pageIndex = newPage;
            RefreshPageLocal();
        }


        void RefreshPageLocal()
        {

            var itemsArray = new DynamicDebugItem[_debugItems.Count];
            _debugItems.Values.CopyTo(itemsArray, 0);

            foreach (var item in itemsArray)
            {
                item.GetTmp.gameObject.SetActive(false);
            }

            int index = _pageIndex * GetItemsDisplayCount;
            int endIndex = Mathf.Min(index + GetItemsDisplayCount, itemsArray.Length);
            for (int i = index; i < endIndex; i++)
            {
                itemsArray[i].GetTmp.gameObject.SetActive(true);
            }

            int displayedNowOnPageCount = endIndex - index;
            _pageIndexText.text = $"P.{_pageIndex} : {displayedNowOnPageCount}/{GetItemsCount}";

        }






        //? EXPOSED
        internal static bool GetAllExtrasVissibility
            => _inst._allExtrasVissibility;

        internal static void AddNewDebugItem(DynamicDebugItem item)
        {

            string duplicateWarning = "CREATED MULTIPLE DYNAMIC DEBUG ITEMS WITH THE SAME CODE : " + item.GetCode + " THIS CAN CAUSE PROBLEMS WITH REFRESHING THE CORRECT ITEM";
#if KARIANAKIS
            if (_inst._debugItems.TryGetValue(item.GetCode, out var debugger))
            {
                Debug.LogError(duplicateWarning);
                return;
            }
#else
            Debug.LogWarning(duplicateWarning);
#endif
            _inst._debugItems[item.GetCode] = item;
        }

        internal static void ForceVissibility(bool value)
            => _inst.SetVissiblity(value);

        internal static DynamicDebugItem GetItemByCode(string code)
        {
            if (_inst._debugItems.TryGetValue(code, out var item))
                return item;
            else
                return null;
        }

        internal static TMPro.TextMeshProUGUI GetTmp(string code)
        {

            GameObject t = Instantiate(_inst._tmpTemplate.gameObject,
                _inst._tmpTemplate.transform.parent);
            t.SetActive(true);
            t.name = "item : " + code;

            int pinnedTopCount = 0;
            foreach (var item in _inst._debugItems)
            {
                if (item.Value.GetPinTop)
                    pinnedTopCount++;
            }
            t.transform.SetSiblingIndex(pinnedTopCount);

            return t.GetComponent
                 <TMPro.TextMeshProUGUI>();

        }

        internal static void InvokeItemsReorder()
                  => invo.simple(_inst.ReorderDebuggItemsDepricated, 0.05f);

        internal static void RefreshPage()
                    => _inst.RefreshPageLocal();




    }
}
