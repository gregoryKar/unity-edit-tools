using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Karianakis.EditTools
{


    public class EditTerminal : MonoBehaviour
    {

        /*
        use input manager that handles the inputs and you
        just link an action to an input .. for now just update

        when text empty displays the top used commands
        */




        static EditTerminal _instForbidden;
        static EditTerminal _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    var obj = new GameObject("EditTerminal");
                    obj.transform.SetParent(EditSuitFather._inst.transform, false);
                    var rect = obj.AddComponent<RectTransform>();
                    rect.anchoredPosition = Vector2.zero;
                    _instForbidden = obj.AddComponent<EditTerminal>();

                    //_instForbidden.CreateGraphics();
                    _instForbidden.CreatePreffabGraphics();
                }


                return _instForbidden;

            }
            set { _instForbidden = value; }



        }


        void CreateGraphicsDepricated()
        {


            return;
            return;
            return;

            var rect = gameObject.AddComponent<RectTransform>();
            rect.name = "EditTerminal";
            //rect.gameObject.AddComponent<EditTerminal>();

            //var canvas = FindFirstObjectByType<Canvas>();




            rect.pivot = new Vector2(0, 0.5f);
            rect.anchorMin = new Vector2(0, .5f);
            rect.anchorMax = new Vector2(0, .5f);

            var _image = new GameObject("Image + Layout").AddComponent<Image>();
            _image.rectTransform.SetParent(rect, false);
            _image.color = new Color(0, 0, 0, 0.9f);
            _image.rectTransform.sizeDelta = new Vector2(150, 150);

            _image.rectTransform.pivot = new Vector2(0, 0.5f);
            _image.rectTransform.anchorMin = new Vector2(0, .5f);
            _image.rectTransform.anchorMax = new Vector2(0, .5f);


            var tmp = new GameObject("Input").AddComponent<TMPro.TextMeshProUGUI>();
            tmp.rectTransform.SetParent(_image.rectTransform, false);
            tmp.rectTransform.sizeDelta = new Vector2(150, 25);
            tmp.rectTransform.anchoredPosition = new Vector2(0, 50);

            tmp.rectTransform.pivot = new Vector2(0, 0.5f);
            tmp.rectTransform.anchorMin = new Vector2(0, .5f);
            tmp.rectTransform.anchorMax = new Vector2(0, .5f);
            tmp.fontSize = 16;

            tmp.text = "---";

            var suggestions = Instantiate(tmp.gameObject, tmp.transform.parent).GetComponent<TMPro.TextMeshProUGUI>();

            suggestions.name = "Suggestions";
            suggestions.text = "text -> press Enter";
            suggestions.color = new Color(1, 1, 1, 0.2f);


            var layout = _image.gameObject.AddComponent<VerticalLayoutGroup>();

            layout.padding = new RectOffset(5, 5, 5, 0);
            layout.childControlHeight = true;
            layout.childControlWidth = true;

            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;



            _consoleObj = _image.gameObject;
            _input = tmp;
            _suggestions = suggestions;


        }


        void CreatePreffabGraphics()
        {
            const string _preffabCode = "EditTerminalLayout";

            var obj = Instantiate(Resources.Load<GameObject>(_preffabCode), transform);
            //obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            EditSuitFather._inst.SetPosRelativeToScreen(obj.GetComponent<RectTransform>()
            , new Vector2(0, 0.5f));

            _consoleObj = obj;
            _input = obj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            _suggestions = obj.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1];



        }




        private void Start()
        {
            AddCommand("test", () => Debug.Log("chat suggested : marinakis is the best .. after i said marinakis"));


            ShortcutAction.Create("EditConsoleToggle", () => ChangePanelOpenCloseState(), _toggleKeys);

            ShortcutAction.CreateOptional("EditTerminal : execute", () => Execute(), KeyCode.KeypadEnter, KeyCode.Return);

            ShortcutAction.Create("EditTerminal : NextSuggestion", () => SelectNextSuggestion(1), KeyCode.DownArrow);

            ShortcutAction.Create("EditTerminal : PreviousSuggestion", () => SelectNextSuggestion(-1), KeyCode.UpArrow);




        }




        KeyCode[] _toggleKeys = new KeyCode[] { KeyCode.Space, KeyCode.Q };
        Color _correctCommandColor = Color.green;
        Color _wrongCommandColor = Color.white;


        GameObject _consoleObj;
        TMPro.TextMeshProUGUI _input;
        TMPro.TextMeshProUGUI _suggestions;

        void Update()
        {



            if (GetConsoleOpenStatus is false) return;

            Process();
            ColorChange();





        }


        void ChangePanelOpenCloseState()
        {
            bool newState = !_consoleObj.activeSelf;
            _consoleObj.SetActive(newState);
        }
        bool GetConsoleOpenStatus => _consoleObj.activeSelf;










        #region //// DEPRICATED - SAVE - LOAD in future



        // const string _prefCode = "EditTerminal";

        // void TestDeletePreffs()
        // {

        //     PlayerPrefs.DeleteKey(_prefCode);

        //     //PlayerPrefs.SetInt(_prefCode + "_showLeft", _showLeft ? 1 : 0);
        //     PlayerPrefs.DeleteKey(_prefCode + "_height");
        //     PlayerPrefs.DeleteKey(_prefCode + "_size");

        // }


        // bool _settingsLoaded = false;
        // void SaveSettings()
        // {

        //     PlayerPrefs.SetInt(_prefCode, 1);

        //     //PlayerPrefs.SetInt(_prefCode + "_showLeft", _showLeft ? 1 : 0);
        //     PlayerPrefs.SetFloat(_prefCode + "_height", _height);
        //     PlayerPrefs.SetFloat(_prefCode + "_size", _size);
        // }



        // void LoadSettings()
        // {

        //     _settingsLoaded = true;
        //     if (transform.GetComponent<RectTransform>() == null) return;

        //     if (!PlayerPrefs.HasKey(_prefCode))
        //     {

        //         var rect2 = gameObject.GetComponent<RectTransform>();
        //         _height = rect2.position.y;
        //         _size = rect2.localScale.x;


        //         SaveSettings();
        //         return;
        //     }

        //     var rect = transform.GetComponent<RectTransform>();
        //     rect.position = new Vector2(rect.position.x, _height);
        //     rect.localScale = new Vector3(_size, _size, 1);





        // }



        #endregion




        void ColorChange()
        {
            if (_input.text.Length <= 0) { }
            else if (_commands.ContainsKey(_input.text))
                _input.color = _correctCommandColor;
            else
                _input.color = _wrongCommandColor;
        }


        void DisplaySuggestions()
        {

            if (_input.text.Length <= 0)
            {
                _suggestions.text = "- - -";
                return;
            }

            _suggestionList = GetTop3ClosestCommands(_input.text);
            _suggestions.text = "- \n ";
            _suggestions.text += string.Join("\n ", _suggestionList);

            _suggestionIndex = -1;

        }

        int _suggestionIndex = -1;
        List<string> _suggestionList = new List<string>();
        void SelectNextSuggestion(int direction)
        {
            if (_suggestionList.Count <= 0) return;

            _suggestionIndex += direction;

            if (_suggestionIndex >= _suggestionList.Count) _suggestionIndex = 0;

            else if (_suggestionIndex < 0) _suggestionIndex = _suggestionList.Count - 1;

            _input.text = _suggestionList[_suggestionIndex];

        }




        void Process()
        {


            foreach (char c in Input.inputString)
            {
                if (c == '\b') // backspace
                {
                    if (_input.text.Length > 0)
                        _input.text = _input.text.Substring(0, _input.text.Length - 1);
                }
                else if (c == '\n' || c == '\r') // enter/return
                {
                    // ExecuteCommand(_input.text);
                    // _input.text = "";
                }
                else
                {
                    _input.text += c;
                    DisplaySuggestions();
                }
            }



        }

        void ClearAllInput()
        {
            _input.text = "";
            DisplaySuggestions();
        }








        void Execute()
        {
            ExecuteCommand(_input.text);
            _input.text = "";
        }


        void ExecuteCommand(string code)
        {

            if (_commands.ContainsKey(code))
                _commands[code].Invoke();
            else
                WrongCommandMessage(code);
            //Debug.LogError("Command not found: " + code);

        }
        Dictionary<string, Action> _commands = new();

        public static void AddCommand(string code, Action action)
        => _inst._commands[code] = action;





        void WrongCommandMessage(string code)
        {
            _suggestionList = GetTop3ClosestCommands(code);
            string message = $"WRONG ('{code}') ->: {string.Join(", ", _suggestionList)} ??";
            Debug.LogError(message);

        }
        List<string> GetTop3ClosestCommands(string input)
        {
            var commandDistances = new List<(string command, int distance)>();

            foreach (var command in _commands.Keys)
            {
                int distance = LevenshteinDistance(input, command);
                commandDistances.Add((command, distance));
            }

            return commandDistances
                .OrderBy(x => x.distance)
                .Take(3)
                .Select(x => x.command)
                .ToList();
        }

        int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; i++)
                d[i, 0] = i;

            for (int j = 0; j <= m; j++)
                d[0, j] = j;

            for (int j = 1; j <= m; j++)
            {
                for (int i = 1; i <= n; i++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }







    }

}
