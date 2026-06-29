



using Karianakis.EditTools;
using UnityEngine;
using TMPro;
using Karianakis.Utilities;

namespace Karianakis.EditTools
{

    class EditToolsShowcase : MonoBehaviour
    {

        /*
        - dynamic debug panel
            through code debugs
            through attributes on variables 
       
        - edit cheat console
            functions attributes
            with params too
            text completion from suggestions
            navigate previous / suggested

        - highlight hierarchy
            item by C# 
            custom colors / icons
        
        - Shortcut manager

        - custom shortcuts that unity didnt have
        
        - settings panel


        */

        [SerializeField] TextMeshProUGUI _showcaseText;
        [SerializeField] float _showcaseTextDuration = 2f;
        MyId _showcaseTextId;
        void SayShowcaseText(string text)
        {

            _showcaseText.text = text;

            if (_showcaseTextId == null) _showcaseTextId = new MyId();
            InvoManager.CancelAll(_showcaseTextId);

            _showcaseText.enabled = true;
            InvoAdvanced.Repeat((invaki) =>
            {

                int index = invaki.GetIterationIndex;
                invaki.SetDelay(0.04f);
                if (index == 0)
                {
                    _showcaseText.enabled = false;
                }
                else if (index == 1)
                {
                    _showcaseText.enabled = true;
                }
                else if (index == 2)
                {
                    _showcaseText.text = "---";
                }
                else if (index == 3)
                {
                    _showcaseText.text = "---";
                }
                else if (index == 4)
                {
                    _showcaseText.text = "------";
                }


            }, _showcaseTextDuration, 5).SetId(_showcaseTextId);
        }

        [SerializeField] GameObject _hierarchyItemToHighlight;


        void Start()
        {
            StartSetDynamicDebugs();
            StartSetShortcuts();
        }






        // to add a new variable to be debuged i just add my attribute
        [DebugVariable("var _timer")]
        float _timer;
        [DebugVariable("var _time_SLL")]
        float _time_SLL;
        [DebugVariable("var _time_T")]
        float _time_T;

        [DebugVariable("var _vek")]
        Vector2 _vek;
        [DebugVariable("var _bool")]
        bool _bool;



        // also i have some optional parameters for extra functionality like having a nickname , controlling the update interval and setting a color
        [DebugVariable("hallo"
         , nickname: "nickaname"
         , interval: 0.5f
         , color: FixedColor.Red)]
        bool _extraOptionsExample;






        void StartSetDynamicDebugs()
        {

            // i can also debug values independant from class variables 

            // here this will make a simple display message with name and content
            DynamicDebug.Create("from code simple")// nickname
            .SetColor(Color.magenta)
            .SetContent("this will appear on the panel");



            // here this way i will create a dynamic debug that updates its content and color and visibility dynamicaly every second 

            // example use case : display distance from player, and when out of range change the color to red or hide completely 
            DynamicDebug.Create("from code dynamic")
            .SetInterval(1f)// updates the value reapeated
            .SetDynamicColor(() => Time.time % 2 == 1 ?
                 Color.red : Color.green)// updates the value reapeated
            .SetDynamicContent(() => "Dynamic content at " +
                 Time.time.ToString("F2"))//gets the new value every timeit gets updated
            .SetDynamicEnabled(() => Random.value > 0.2f);// controll if vissible



        }







        void StartSetShortcuts()
        {
            // just like that now theis function will be called after i press space + S
            ShortcutAction.Create("test shortcut", ShortcutFunction, KeyCode.Space, KeyCode.S);
        }














        void Update()
        {
            _timer += Time.deltaTime;
            _time_SLL = Time.timeSinceLevelLoad;
            _time_T = Time.time;
        }





        [ConsoleCommand()]
        void HighlightHierarchyItemError()
        {
            SayShowcaseText("highlight hierarchy item called");
            StyledHierarchyItem.HighlightError(_hierarchyItemToHighlight);
            //StyledHierarchyItem.HighlightGreen(_hierarchyItemToHighlight);
        }
        [ConsoleCommand()]
        void HighlightHierarchyItemCustom()
        {
            SayShowcaseText("highlight hierarchy item called");
            StyledHierarchyItem.HighlightCustom(_hierarchyItemToHighlight, Color.black, Color.yellow);
        }






        // here i just add the attribute and now this function is available in the console for testing
        // exacly the same for multiple parameters as well, its that simple to use 
        [ConsoleCommand()]
        void ShowcaseFunction() { SayShowcaseText("showcase function called"); }

        [ConsoleCommand()]
        void ShowcaseFunctionParam(int param) { SayShowcaseText("showcase function with param called: " + param); }

        [ConsoleCommand()]
        void ShowcaseFunctionMultiParam(int param, string text, float value) { SayShowcaseText($"showcase function with multiple params called: {param}, {text}, {value}"); }









        void ShortcutFunction()
        {
            SayShowcaseText("shortcut function called at " + Time.time.ToString("F2"));
        }



    }
}