using System;
using System.Collections.Generic;
using System.Linq;
using Karianakis.Utilities;
using UnityEngine;
using UnityEngine.UI;

// #if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
// using UnityEngine.InputSystem;
// #endif


namespace Karianakis.EditTools
{
    internal class CommandTerminal : MonoBehaviour
    {
        static CommandTerminal _instForbidden;
        static CommandTerminal _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    var obj = SpawnMyself;
                    obj.name = "CommandTerminal";
                    obj.transform.SetParent(EditSuitFather.GetCanvas(), false);

                    _instForbidden = obj.AddComponent<CommandTerminal>();

                    _instForbidden.ConfigureRect(obj);
                    _instForbidden.ConfigureStuff(obj);

                }
                return _instForbidden;
            }
        }


        //? PARAMS
        const string _preffabCode = "EditTerminalLayout";
        const string _visibilityKey = "CommandTerminalVisibility";

        const int _maxSuggestionLines = 5;
        const float _sizeIncreaseAmount = 0.2f;


        KeyCode[] _toggleKeys = new KeyCode[] { KeyCode.Space, KeyCode.Q };

        RectTransform _containerRect;
        GameObject _kidContainerObj;
        CommandTerminalReferences _references;
        TerminalLogs _terminalLogs;


        timeStamp _lastExecuteTime;

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
                if (_inputWords.Length < 1) _inputWords = new string[] { " " };

                RefreshTextGraphicsForbidden();
                RefreshSuggestions();
            }
        }
        string[] _inputWords;

        [SerializeField] Char _seperatorDisplayChar = '_';
        [SerializeField] String _emptyInputDisplay = "-----";

        [SerializeField] Color _commandColor = Color.green;
        [SerializeField] Color _errorColor = Color.red;
        [SerializeField] Color _numberColor = Color.yellow;
        [SerializeField] Color _booleanColor = Color.magenta;
        [SerializeField] Color _stringColor = Color.blue;

        Dictionary<Type, Color> _typeColors = new Dictionary<Type, Color>();

        int _suggestionIndex = -1;
        List<string> _suggestionList = new List<string>();
        List<CustomCommand> _commands = new();
        float _referenceDeltaSizeX;


        //? GETTERS
        bool GetVisibilityStatus => gameObject.activeSelf;
        bool GetInputIsEmpty => _inputWords.Length < 1 || _inputWords[0] == " ";

        static GameObject SpawnMyself
                   => Instantiate(Resources.Load<GameObject>(_preffabCode));


        //? MAIN
        void ConfigureRect(GameObject obj)
        {
            _kidContainerObj = obj.transform.GetChild(0).gameObject;
            _containerRect = obj.GetComponent<RectTransform>();

            RectTransform rect = obj.GetComponent<RectTransform>();

            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 1);

            rect.offsetMin = new Vector2(rect.offsetMin.x, -6);
            rect.offsetMax = new Vector2(rect.offsetMax.x, 6);

            Vector2 anchorePos = rect.anchoredPosition;
            anchorePos.x = -6;
            rect.anchoredPosition = anchorePos;

            Vector2 sizeDelta = rect.sizeDelta;
            sizeDelta.x = 220;
            rect.sizeDelta = sizeDelta;

            _referenceDeltaSizeX = rect.sizeDelta.x;

            float size = EditToolsSettings.GetTerminalSize;
            SetTransformSize(size);


        }

        void ConfigureStuff(GameObject obj)
        {

            _references =
                 obj.GetComponent<CommandTerminalReferences>();

            _terminalLogs = new TerminalLogs
                (_references.GetLogsAboveText);
            _terminalLogs.AddLogLine("logs initiated", "--", color: Color.cyan);

            _references.GetToggleShortcutText.text = $"Toggle : {string.Join(" + ", _toggleKeys.Select(k => k.ToString()))}";

            _references.GetSettingsButton.GetComponent<Button>().onClick
               .AddListener(() => EditToolsSettingsProvider.OpenSettingsPanel());

            _references.GetSizeIncreaseButton.GetComponent<Button>().onClick
                    .AddListener(() => ChangeTransformSize(_sizeIncreaseAmount));
            _references.GetSizeDecreaseButton.GetComponent<Button>().onClick
                    .AddListener(() => ChangeTransformSize(-_sizeIncreaseAmount));


        }

        private void Start()
        {

            rawInput = "";

            _typeColors[typeof(int)] = _numberColor;
            _typeColors[typeof(float)] = _numberColor;
            _typeColors[typeof(bool)] = _booleanColor;
            _typeColors[typeof(string)] = _stringColor;



            // AddCommand("test", () => Debug.Log("chat suggested : marinakis is the best .. after i said marinakis"));


            ShortcutAction.Create("EditConsoleToggle", () => ReverseVisibility(), _toggleKeys);



            ShortcutAction.Create("EditTerminal : NextSuggestion Tab", () => SelectNextSuggestion(1), KeyCode.Tab);

            ShortcutAction.Create("EditTerminal : NextSuggestion", () => SelectNextSuggestion(1), KeyCode.DownArrow);

            ShortcutAction.Create("EditTerminal : PreviousSuggestion", () => SelectNextSuggestion(-1), KeyCode.UpArrow);

            ShortcutAction.Create("EditTerminal : previousCommand", () =>
            rawInput = _terminalLogs.GetNextFromLogs(-1), KeyCode.LeftArrow);

            ShortcutAction.Create("EditTerminal : nextCommand", () => rawInput = _terminalLogs.GetNextFromLogs(1), KeyCode.RightArrow);

            StartSetVissibility();

            CustomCommand.Simple("clc", _terminalLogs.ClearLogs);
            CustomCommand.Simple("clear", _terminalLogs.ClearLogs);
            CustomCommand.Simple("printAll", PrintAllCommands);



            // CommandBuilderUniversal.Create("fromDelegate"
            // , (Action<int, string>)((a, b) => { Debug.Log($"{a} {b}"); }));

            // CommandBuilderUniversal.Create("labda"
            //        , () => { Debug.Log($"--"); });
            // CommandBuilderUniversal.Create("labdaTwoParams"
            //        , (int a, int b) => { Debug.Log($"{a} {b}"); });

            rawInput = "";// to refresh the suggestions and logs and all that stuff and make sure everything is ok at the start

        }

        void Update()
        {

            if (GetVisibilityStatus is false) return;

            if (InputManager.GetAnyKeyAnyState() == false) return;



            if (InputManager.GetKeyDown(KeyCode.KeypadEnter)
            || InputManager.GetKeyDown(KeyCode.Return))
            {
                Execute();
                return;
            }
            if (InputManager.GetKey(KeyCode.Backspace))
            {
                if (rawInput.Length < 1) return;
                if (InputManager.GetKey(KeyCode.Space))
                    rawInput = "";
                else
                    rawInput = rawInput.Substring(0, rawInput.Length - 1);

                return;
            }

            if (InputManager.GetCharsPressedNow
                          (out char[] chars) == false) return;

            foreach (char c in chars)
            {
                rawInput += c;
            }

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
            SetVisiblity(startVissibility);

            // check and open / close +
            // set the visibility last visibility acordingly
            // now need double tap tp change id start open
        }




        //? FUNCTIONS
        void ChangeTransformSize(float change)
        {
            float valueBefore = _kidContainerObj.transform.localScale.x;
            float newValue = valueBefore + change;

            newValue = EditToolsSettings.ClampTerminalSize(newValue);
            EditToolsSettings.SetTerminalSize(newValue);

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

        bool TryGetSelectedCommand(out CustomCommand command)
        {
            command = null;
            if (_inputWords.Length < 1) return false;

            foreach (var cmd in _commands)
            {
                if (cmd.GetCode == _inputWords[0])
                {
                    command = cmd;
                    return true;
                }
            }

            return false;
        }


        void PrintAllCommands()
        {
            string allCodes = string.Join(", ", _commands.Select(c => c.GetCode));
            Debug.Log($"ALL {_commands.Count} COMMANDS : " + allCodes);


            for (int i = 0; i < _commands.Count; i++)
            {
                _terminalLogs.AddLogLine(_commands[i].GetCode, $"{i}-");
            }

        }


        void RefreshTextGraphicsForbidden()
        {
            if (rawInput.Length < 1)
            {
                _references.GetInputDisplayText.text = _emptyInputDisplay;
                DisplayFunctionParams();
                return;
            }

            var inputWordsClone = _inputWords.Clone() as string[];
            var outputWords = GetColorisedWords(inputWordsClone);


            if (outputWords.Length <= 1)
            {
                _references.GetInputDisplayText.text = outputWords[0];
            }
            else
            {
                _references.GetInputDisplayText.text = string
                    .Join(_seperatorDisplayChar, outputWords.ToArray());
            }

            DisplayFunctionParams();

        }

        void DisplayFunctionParams()
        {
            Type[] paramTypes = null;
            if (TryGetSelectedCommand
                (out var selectedCommand))
            {
                paramTypes = selectedCommand.GetParameterTypes;
            }
            else
            {
                _references.GetParamsShowcaseText.text = "---";
                return;
            }

            if (paramTypes == null || paramTypes.Length < 1)
            {
                _references.GetParamsShowcaseText.text = "---";
                return;
            }
            else
            {

                string[] showcaseArray = new string[paramTypes.Length];
                for (int i = 0; i < paramTypes.Length; i++)
                {
                    if (ParamIsCorrect(i))
                    {
                        showcaseArray[i] = TextUtilities.WrapWordWithColor(paramTypes[i].Name, Color.green);
                    }
                    else
                    {
                        showcaseArray[i] = $"{paramTypes[i].Name}";
                    }

                }

                _references.GetParamsShowcaseText.text = string.Join(" ", showcaseArray);
            }



            bool ParamIsCorrect(int index)
            {
                if (index >= _inputWords.Length - 1) return false;
                return TypesUtilities.TryConvertStringToType(_inputWords[index + 1], paramTypes[index], out _, true);
            }



        }

        string[] GetColorisedWords(string[] wordsCopy)
        {



            if (CheckIfWordIsCommand(wordsCopy[0]))
            {
                wordsCopy[0] = TextUtilities.WrapWordWithColor(wordsCopy[0], _commandColor);

            }
            else
            {
                wordsCopy[0] = TextUtilities.WrapWordWithColor(wordsCopy[0], _errorColor);

            }

            //_typeColors
            Type[] paramTypes = null;
            if (TryGetSelectedCommand
                (out var selectedCommand))
            {
                paramTypes = selectedCommand.GetParameterTypes;
            }

            for (int i = 1; i < wordsCopy.Length; i++)
            {


                ColoriseByType(typeof(bool), wordsCopy[i], i);
                ColoriseByType(typeof(int), wordsCopy[i], i);
                ColoriseByType(typeof(float), wordsCopy[i], i);
                ColoriseByType(typeof(string), wordsCopy[i], i);


            }


            void ColoriseByType(Type type, string word, int index)
            {
                bool typeMatches = TypesUtilities
                    .TryConvertStringToType(word, type, out _, true);

                if (typeMatches && _typeColors.TryGetValue(type, out var color))
                {
                    wordsCopy[index] = TextUtilities.WrapWordWithColor(wordsCopy[index], color);
                }
            }
            return wordsCopy;

        }

        bool CheckIfWordIsCommand(string code)
        {
            foreach (var command in _commands)
            {
                if (command.GetCode == code) return true;
            }
            return false;
        }

        void SetVisiblity(bool visible)
        {
            gameObject.SetActive(visible);
            PlayerPrefs.SetInt(_visibilityKey, visible ? 1 : 0);
        }
        void ReverseVisibility()
        {
            bool newState = !GetVisibilityStatus;
            SetVisiblity(newState);
        }


        void Execute()
        {
            if (GetInputIsEmpty) return;
            if (_lastExecuteTime.HasThatAmountPassed(0.2f) == false) return;
            _lastExecuteTime.setNow();

            var wordsList = _inputWords.ToList();
            wordsList.RemoveAt(0);

            string[] variables;
            if (wordsList.Count > 0) variables = wordsList.ToArray();
            else variables = new string[0];

            // Debug.LogError("will execute command : " + _inputWords[0] + " with variables : " + string.Join(", ", variables));


            ExecuteCommand(_inputWords[0], variables);
            rawInput = "";
        }

        void ExecuteCommand(string code, string[] variables)
        {

            var command = _commands.FirstOrDefault(c => c.GetCode == code);
            if (command == null)
            {
                _terminalLogs.AddLogLine($"Command not found: <color=red>{code}</color>", "!= ", color: Color.magenta);
                return;
            }


            //.TryExecuteStrings(variables);
            bool executedFine = command.TryExecuteStrings(variables);

            if (executedFine == false)
            {
                _terminalLogs.AddLogLine(code, "X ", color: Color.red);
            }
            else
            {
                _terminalLogs.AddLogLine(code, "> ");
            }


        }


        //! PUT TO TEXT UTILITIES
        void RefreshSuggestions()
        {

            string firstWord = _inputWords[0];
            if (_suggestionList.Contains(rawInput)
                || _suggestionList.Contains(firstWord)) return;

            _suggestionIndex = -1;
            _suggestionList.Clear();

            if (rawInput.Length < 1)
            {
                _suggestionList = GetRandomCommands(_maxSuggestionLines);
            }
            else
            {
                _suggestionList = GetClosestCommands(firstWord, _maxSuggestionLines);
            }



            DisplaySuggestions();

        }
        void DisplaySuggestions()
        {

            var displayArray = _suggestionList.ToArray();

            if (_suggestionIndex >= 0 && _suggestionIndex < displayArray.Length)
            {
                displayArray[_suggestionIndex] = TextUtilities.WrapWordWithColor(displayArray[_suggestionIndex], Color.yellow);
            }

            for (int i = 0; i < displayArray.Length; i++)
            {
                displayArray[i] = $"{i} : {displayArray[i]}";
            }


            //_suggestionsText.text = "- \n ";
            _references.GetSuggestionsText.text = string.Join("\n", displayArray);

        }

        void SelectNextSuggestion(int direction)
        {
            if (_suggestionList.Count <= 0) return;

            _suggestionIndex += direction;

            if (_suggestionIndex >= _suggestionList.Count)
            {
                _suggestionIndex = 0;
            }
            else if (_suggestionIndex < 0)
            {
                _suggestionIndex = _suggestionList.Count - 1;
            }

            rawInput = _suggestionList[_suggestionIndex];
            DisplaySuggestions();
        }



        List<string> GetClosestCommands(string input, int maxAmount)
        {
            var commandDistances =
                new HashSet<(string command, int distance)>();

            foreach (var command in _commands)
            {
                int distance = LevenshteinDistance(input, command.GetCode);
                commandDistances.Add((command.GetCode, distance));
            }

            maxAmount = Mathf.Min(maxAmount, commandDistances.Count);

            return commandDistances
                .OrderBy(x => x.distance)
                .Take(maxAmount)
                .Select(x => x.command)
                .ToList();
        }
        List<string> GetRandomCommands(int amount)
        {
            var randomCommands = new HashSet<string>();
            var random = new System.Random();

            int counter = 0;
            while (randomCommands.Count < amount && counter < 100)
            {
                int index = random.Next(_commands.Count);
                randomCommands.Add(_commands[index].GetCode);

                counter++;
            }

            return randomCommands.ToList();
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




        // //? EXPOSED
        // public static void AddCommand(string code, Action action)
        // {
        //     var command = CustomCommand.Simple(code, action);
        //     _inst._commands.Add(command);
        // }
        // public static void AddCommand<T1>(string code, Action<T1> action)
        // {
        //     var command = CustomCommand.OneParam<T1>(code, action);
        //     _inst._commands.Add(command);
        // }
        // public static void AddCommand<T1, T2>(string code, Action<T1, T2> action)
        // {
        //     var command = CustomCommand.TwoParam<T1, T2>(code, action);
        //     _inst._commands.Add(command);
        // }
        // public static void AddCommand<T1, T2, T3>(string code, Action<T1, T2, T3> action)
        // {
        //     var command = CustomCommand.ThreeParam<T1, T2, T3>(code, action);
        //     _inst._commands.Add(command);
        // }


        internal static void AddCommandInternal(CustomCommand command)
        {
            _inst._commands.Add(command);
        }






    }

}
