


using System;
using System.Text;
using Karianakis.EditTools;
using Karianakis.Utilities;
using UnityEngine;

namespace Karianakis.EditTools
{

    public partial class DynamicDebugItem
    {


        private DynamicDebugItem(string code, TMPro.TextMeshProUGUI tmp)
        {
            _code = code;
            _nickname = code;
            _color = GetDefaultTittleColor;

            _tmp = tmp;
            _tmp.text = "from code : " + code;
            _tmp.color = GetDefaultContentColor;

            //_karianakisTag = KarianakisTagManager.GetDefaultTag;
            KarianakisTagManager.SubscribeToTagsChanged(Refresh);
            DynamicDebugManager.InvokeItemsReorder();
            DynamicDebugManager.AddNewDebugItem(this);
        }

        /*
        NON PLAY MODE RETURNS NULL - SINGLE ENTRY POINT 
        on creation checks if other with same code
        exist , if yes returns the existing one 
        gets textMeshPro
         */
        static internal DynamicDebugItem Create(string code)
        {

            if (Application.isPlaying == false)
            {
                Debug.LogError("DynamicDebugItem can only be created in play mode");
                return null;
            }

            DynamicDebugItem item =
                DynamicDebugManager.GetItemByCode(code);

            if (item != null)
                return item;
            else
                return new DynamicDebugItem(code, DynamicDebugManager.GetTmp(code));
        }






        string _code;
        //code used to refernce the item like an id
        string _nickname;
        //by default equals to code if not changed .. is the main reference
        //  to be used for display like -> nikos : 0.221f
        //!KarianakisTag _karianakisTag;
        // used for grouping and stuff like disable tag /
        // show only this tag
        string _content; // why needed to be stored ??
                         // px you do tmp = content + prnint count ..
        Color _color;


        Func<bool> _GetEnabled;
        Func<Color> _Getcollor;
        Func<string> _GetContent;
        TMPro.TextMeshProUGUI _tmp;




        myId _id;
        //bool _printCount;
        int _updateCount = 0;
        int _lastFrame;
        float _lastTime;
        float _interval;
        bool _pinTop;
        bool _pinBottom;

        Color GetDefaultTittleColor => new Color(0.1f, 0.8f, 0.8f);
        Color GetDefaultContentColor => new Color(0.9f, 0.9f, 0.9f);

        //int GetSecondLineSizePercent => 80;
        Color GetSecondLineColor => Color.gray;





        internal string GetCode => _code;
        internal string GetNickname => _nickname;
        internal int GetUpdateCount => _updateCount;
        internal float GetInterval => _interval;


        internal bool GetPinTop => _pinTop;
        internal bool GetPinBottom => _pinBottom;
        internal TMPro.TextMeshProUGUI GetTmp => _tmp;


        internal void Refresh()
        {

            //todo decide
            // Get enabled and Tag Enabled relationship ???
            bool enabled;

            bool getEnabledPresent = _GetEnabled != null;
            bool getEnabledStatus =
                getEnabledPresent ? _GetEnabled() : false;

            //bool tagEnabledStatus = _karianakisTag.GetEnabled;


            if (getEnabledPresent) enabled = getEnabledStatus;
            else enabled = true;
          

            _tmp.enabled = enabled;



            if (_Getcollor != null) _color = _Getcollor();
            if (_GetContent != null) _content = _GetContent();

            //_tmp.color = _color;

            _updateCount++;
            _lastFrame = Time.frameCount;
            _lastTime = Time.timeSinceLevelLoad;

            SetFinalText();




            //if (_printCount) _tmp.text += " (" + _updateCount + ")";
        }





        void SetFinalText()
        {

            string startContent = _nickname + " - "; ;
            string finalText = startContent + _content;

            if (DynamicDebugManager.GetAllExtrasVissibility)//end info or other extras to be printed
            {

                bool keepMinimal = true;
                string ending;

                ending = keepMinimal ? "c" : "count";
                finalText += $" ({_updateCount}.{ending}) ";
            
                ending = keepMinimal ? "t" : "time";
                finalText += $"({_lastTime:F1}.{ending}) ";
          
                ending = keepMinimal ? "f" : "frame";
                finalText += $"({_lastFrame}.{ending}) ";
          
            }


            _tmp.text = finalText;

            TextUtilities.StyleAfterFirstLine
                                       (_tmp
                                       , GetSecondLineColor);

            string tittleColor = ColorUtility.ToHtmlStringRGB(_color);
            string afterRenderText = _tmp.text;

            // add after start
            // add at start

            afterRenderText = afterRenderText
                .Insert(startContent.Length
                , "</color>");
            afterRenderText = afterRenderText
                .Insert(0
            , $"<color=#{tittleColor}>");


            _tmp.text = afterRenderText;



        }





    }


}