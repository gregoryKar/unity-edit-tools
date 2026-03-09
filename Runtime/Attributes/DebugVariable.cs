using System;
using UnityEngine;

namespace Karianakis.EditTools
{


    [AttributeUsage(AttributeTargets.Field)]//, AllowMultiple = true
    public class DebugVariable : Attribute
    {
        public DebugVariable(string code
        , string nickname = default
        , float interval = -1
        , FixedColor color = FixedColor.none
        )
        //, bool tagThisGameobject = false
        //, string tag = default
        {
            _code = code;

            //_tagThisGameobject = tagThisGameobject;

            if (!string.IsNullOrEmpty(nickname)) _hasNickname = true;
          
            if (interval > 0)
            {
                _hasInterval = true;
                _interval = interval;
            }
            //if (tag != default) _hasTag = true;

            if (color != FixedColor.none)
            {
                _hasColor = true;
                SetColorFromEnum(color);
            }


        }



        private string _nickname;
        private string _code;
        private float _interval;
        private Color _color;
        //private string _tag;
        //bool _tagThisGameobject = false;

        private bool _hasNickname, _hasInterval, _hasColor;//, _hasTag


        internal string GetNickname => _nickname;
        internal string GetCode => _code;
        internal float GetInterval => _interval;
        internal Color GetColor => _color;
        //internal string GetTag => _tag;
        //internal bool GetTagThisGameobject => _tagThisGameobject;


        internal bool GetHasNickname => _hasNickname;
        internal bool GetHasInterval => _hasInterval;
        internal bool GetHasColor => _hasColor;
        //internal bool GetHasTag => _hasTag;





        void SetColorFromEnum(FixedColor fixedColor)
        {

            switch (fixedColor)
            {
                case FixedColor.Red:
                    _color = Color.red;
                    //todo_color = MyTestSetting.GetRedColor;
                    break;
                case FixedColor.Green:
                    _color = Color.green;
                    break;
                case FixedColor.Blue:
                    _color = Color.blue;
                    break;
                case FixedColor.Yellow:
                    _color = Color.yellow;
                    break;
                case FixedColor.Magenta:
                    _color = Color.magenta;
                    break;
                case FixedColor.Cyan:
                    _color = Color.cyan;
                    break;
                case FixedColor.White:
                    _color = Color.white;
                    break;
                case FixedColor.Grey:
                    _color = Color.grey;
                    break;
                case FixedColor.Black:
                    _color = Color.black;
                    break;

            }
        }


    }
}
