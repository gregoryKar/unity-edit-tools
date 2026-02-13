using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Karianakis.Utilities;


namespace Karianakis.EditTools
{

    public class DynamicDebugManager : MonoBehaviour
    {

        /*FUTURE IDEAS
            shortcut to toggle all debug items on/off
            list of strings to disable temporarily?? 
    

        */




        static DynamicDebugManager _instForbidden;
        static DynamicDebugManager _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    var obj = new GameObject("DynamicDebugManager");
                    var rect = obj.AddComponent<RectTransform>();
                    rect.anchoredPosition = Vector2.zero;
                    _instForbidden = obj.AddComponent<DynamicDebugManager>();
                    obj.transform.SetParent(EditSuitFather._inst.transform, true);
                    //_instForbidden.CreateGraphics();
                    _instForbidden.CreatePreffabGraphics();
                }

                return _instForbidden;

            }
            set { _instForbidden = value; }



        }



        const string _preffabCode = "DebuggDebuggLayout";

        void CreateGraphicsDepricated()
        {
            return;
            return;
            return;

            var rect = gameObject.AddComponent<RectTransform>();
            rect.name = "DynamicDebugManager";
            rect.pivot = new Vector2(0, 1f);
            rect.anchorMin = new Vector2(0, 1f);
            rect.anchorMax = new Vector2(0, 1f);

            rect.position = new Vector2(0, 0);

            var _image = new GameObject("Image").AddComponent<Image>();
            _image.rectTransform.SetParent(rect, false);
            _image.color = new Color(0, 0, 0, 0.8f);
            _image.rectTransform.sizeDelta = new Vector2(220, 250);

            _image.rectTransform.pivot = new Vector2(0, 1f);
            _image.rectTransform.anchorMin = new Vector2(0, 1f);
            _image.rectTransform.anchorMax = new Vector2(0, 1f);
            _image.rectTransform.position = new Vector2(0, 0);

            var layout = _image.gameObject.AddComponent<VerticalLayoutGroup>();
            layout.childControlHeight = false;
            layout.spacing = 0;




            var tmp = new GameObject("Input").AddComponent<TMPro.TextMeshProUGUI>();
            tmp.rectTransform.SetParent(_image.rectTransform, false);
            tmp.rectTransform.sizeDelta = new Vector2(150, 50);
            //tmp.rectTransform.anchoredPosition = new Vector2(0, 50);

            tmp.rectTransform.pivot = new Vector2(0, 0.5f);
            tmp.rectTransform.anchorMin = new Vector2(0, .5f);
            tmp.rectTransform.anchorMax = new Vector2(0, .5f);
            tmp.fontSize = 22;

            tmp.text = "template";

            tmp.gameObject.SetActive(false);


            _tmpTemplate = tmp;
            _containerObj = _image.gameObject;

        }

        void CreatePreffabGraphics()
        {
            var obj = Instantiate(Resources.Load<GameObject>(_preffabCode), transform);

            EditSuitFather._inst.SetPosRelativeToScreen(obj.GetComponent<RectTransform>()
         , new Vector2(1, 1f));

            _tmpTemplate = obj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            _tmpTemplate.gameObject.SetActive(false);
            _containerObj = obj;

        }


        void Start()
        {
            ShortcutAction.Create("EditDebuggerToggle", () => ChangeVissibility(), _toggleKeys);
        }


        KeyCode[] _toggleKeys = new KeyCode[] { KeyCode.Space, KeyCode.W };




        bool _vissible;
        bool _lastVissible;
        void ChangeVissibility()
        {
            _vissible = !_vissible;
            refreshVissibility();
            _lastVissible = _vissible;
        }
        void refreshVissibility()
            => _containerObj.SetActive(_vissible);



        GameObject _containerObj;
        TMPro.TextMeshProUGUI _tmpTemplate;
        TMPro.TextMeshProUGUI MakeNewTmp()
        {

            GameObject go = Instantiate(_tmpTemplate.gameObject, _tmpTemplate.transform.parent);
            go.SetActive(true);



            return go.GetComponent
            <TMPro.TextMeshProUGUI>();

        }



        private Dictionary<string, DynamicDebugger> _debugClassList = new();

        public static void Say(string code, string content, bool printCount = false, Color color = default)
        => _inst.SayLocal(code, content, printCount, color);

        void SayLocal(string code, string content, bool printCount, Color color = default)
        {



            if (_debugClassList.TryGetValue(code, out var debugger) is false)
            {

                //Debug.LogWarning("mine_" + code + " : " + content);
                new DynamicDebugger(code, content, printCount: printCount, color: color);
                // debugger._printCount = printCount;
                // debugger.Refresh();

            }
            else debugger.RefreshExternal(content, color);



        }


        public static void AddDynamicDebugItem(DynamicDebugger item, string content, float interval)
        {

            Debug.LogWarning("mine_" + item._code + " : " + content);

            item._tmp = _inst.MakeNewTmp();
            item._tmp.text = content;
            item._tmp.name = item._code;

            _inst._debugClassList[item._code] = item;

            _inst.refreshVissibility();

            if (interval >= 0) invo.infinite(() =>
            {
                item.Refresh();
            }, interval);



        }




    }

    public class DynamicDebugger
    {


        public string _code;
        public TMPro.TextMeshProUGUI _tmp;

        Func<bool> _GetEnabled;
        Func<Color> _Getcollor;
        Func<string> _GetContent;

        string _content;
        Color _color;

        public bool _printCount;
        int _updateCount = 0;
        bool _lastEnabledState = true;

        public void Refresh()
        {
            bool enabled = true;
            if (_GetEnabled != null) enabled = _GetEnabled();
            if (enabled != _lastEnabledState)
            {
                _tmp.enabled = enabled;
                _lastEnabledState = enabled;
                _tmp.transform.SetAsLastSibling();
            }



            if (_Getcollor != null) _tmp.color = _Getcollor();
            if (_GetContent != null) _content = _GetContent();

            _tmp.text = _content;

            _updateCount++;
            if (_printCount) _tmp.text += " (" + _updateCount + ")";
        }

        public void RefreshExternal(string content, Color color = default)
        {
            _content = content;
            if (color != default) _color = color;
            Refresh();

        }

        public DynamicDebugger(string code, string content, float interval = -1
        , bool printCount = false, Color color = default,
               Func<bool> Getvissible = null,
               Func<Color> Getcollor = null,
               Func<string> GetContent = null)
        {
            _code = code;

            _content = content;

            _GetEnabled = Getvissible;
            _Getcollor = Getcollor;
            _GetContent = GetContent;

            DynamicDebugManager.AddDynamicDebugItem(this, content, interval);
            _printCount = printCount;

            if (color != default) _tmp.color = color;

        }





    }


}
