using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Karianakis.EditTools
{


    public class EditTerminal : MonoBehaviour
    {


        //! SET ALL TO LOWER CASE INTERNALY

        /*
        use input manager that handles the inputs and you
        just link an action to an input .. for now just update

        when text empty displays the top used commands
        */


        // inputText from raw to fancy



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


        void CreatePreffabGraphics()
        {
            const string _preffabCode = "EditTerminalLayout";

            var obj = Instantiate(Resources.Load<GameObject>(_preffabCode), transform);
            //obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            EditSuitFather._inst.SetPosRelativeToScreen(obj.GetComponent<RectTransform>()
            , new Vector2(0, 0.5f));

            _consoleObj = obj;
            _inputDisplayText = obj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            _suggestions = obj.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1];



        }




        private void Start()
        {

            rawInput = "";
            _inputWords = new string[] { " " };




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

        string _rawInputForbidden;
        string rawInput
        {
            get
            {
                return _rawInputForbidden;
            }
            set
            {
                _rawInputForbidden = value;
                _inputWords = _rawInputForbidden.Split(' ', '_');
                RefreshTextGraphicsForbidden();
                Colorise();


                DisplaySuggestionsForbidden();

            }
        }


        string[] _inputWords;
        TMPro.TextMeshProUGUI _inputDisplayText;
        TMPro.TextMeshProUGUI _suggestions;

        void RefreshTextGraphicsForbidden()
        {
            if (rawInput.Length < 1)
            {
                _inputDisplayText.text = "-- write here noob --";
                return;
            }

            var outputWords = Colorise();


            if (outputWords.Length <= 1)
            {
                _inputDisplayText.text = outputWords[0];
                // if (rawInput.Last() == ' ' || rawInput.Last() == '_') { _inputDisplayText.text += "<color=grey>_"; }
            }
            else
            {
                _inputDisplayText.text = string.Join("_", outputWords.ToArray());

                // if (rawInput.Last() == ' ' || rawInput.Last() == '_') { _inputDisplayText.text += "_"; }


            }

        }



        [SerializeField] Color _commandColor = Color.green;
        [SerializeField] Color _tagCollor = Color.blue;
        [SerializeField] Color _numberColor = Color.green;
        [SerializeField] Color _booleanColor = Color.red;
        string[] Colorise()
        {

            string[] coloredWords = _inputWords.ToArray();

            //! TO GLOBAL STRING UTILITIES
            string WrapWordWithColor(string word, Color col)
              => $"<color=#{ColorUtility.ToHtmlStringRGB(col)}>{word}</color>";


            //first word for command
            if (_commands.ContainsKey(_inputWords[0]))
            {
                coloredWords[0] = WrapWordWithColor(coloredWords[0], _commandColor);
            }

            //tags
            for (int i = 1; i < _inputWords.Length; i++)
            {
                if (KarianakisTagManager.CheckTagExists(_inputWords[i])) coloredWords[i]
                = WrapWordWithColor(coloredWords[i], _tagCollor);
            }

            //numbers
            for (int i = 1; i < _inputWords.Length; i++)
            {
                if (int.TryParse(_inputWords[i], out var integeri))
                    coloredWords[i]
                    = WrapWordWithColor(coloredWords[i], _numberColor);
                else if (float.TryParse(_inputWords[i], out var flotari))
                    coloredWords[i]
                    = WrapWordWithColor(coloredWords[i], _numberColor);
            }

            //true/false
            for (int i = 1; i < _inputWords.Length; i++)
            {

                if (_inputWords[i].ToLower() == "true" || _inputWords[i].ToLower() == "false") coloredWords[i] = WrapWordWithColor(coloredWords[i], _booleanColor);

            }

            return coloredWords;







        }


        void Update()
        {



            if (GetConsoleOpenStatus is false) return;

            Process();





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







        void DisplaySuggestionsForbidden()
        {

            if (_suggestionList.Contains(rawInput)) return;
            if (rawInput.Length < 1) return;

            string firstWord = _inputWords[0];
            // _inputRaw.Split(' ')[0];

            _suggestionList = GetTop3ClosestCommands(firstWord);
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


            rawInput = _suggestionList[_suggestionIndex];


        }




        void Process()
        {


            foreach (char c in Input.inputString)
            {
                if (c == '\b') // backspace
                {
                    if (rawInput.Length > 0)
                        rawInput = rawInput.Substring(0, rawInput.Length - 1);
                }
                else if (c == '\n' || c == '\r') // enter/return
                {
                    // ExecuteCommand(_input.text);
                    // _input.text = "";
                }
                else rawInput += c;



            }



        }

        void ClearAllInput()
             => rawInput = "";




        void Execute()
        {
            if (rawInput.Length < 1) return;


            var wordsList = rawInput.Split(' ').ToList();
            wordsList.RemoveAt(0);

            string[] variables;
            if (wordsList.Count > 0) variables = wordsList.ToArray(); else variables = new string[0];

            for (int i = 0; i < variables.Length; i++)
            {
                variables[i] = variables[i].ToLower();
            }

            ExecuteCommand(_inputWords[0], variables);
            rawInput = "";
        }


        void ExecuteCommand(string code, string[] variables)
        {

            if (_commands.ContainsKey(code.ToLower()))
                _commands[code.ToLower()].Invoke(variables);
            else
                WrongCommandMessage(code);
            //Debug.LogError("Command not found: " + code);

        }
        Dictionary<string, Action<string[]>> _commands = new();


        public static void AddCommand(string code, Action action)
        => _inst._commands[code.ToLower()] = (args) => { action.Invoke(); };
        public static void AddCommand(string code, Action<string[]> action)
        => _inst._commands[code.ToLower()] = action;




        //! PUT TO TEXT UTILITIES

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
