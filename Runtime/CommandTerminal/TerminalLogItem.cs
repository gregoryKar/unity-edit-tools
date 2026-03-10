using System;
using UnityEngine;


namespace Karianakis.EditTools
{
    [Serializable]
    class LogItem
    {
        public LogItem(string content, string prefix, Color color)
        {

            _content = content;
            _color = color;
            _customColor = color != default;

            _prefix = FilterPrefix(prefix);

            _highlighted = false;
        }
        string FilterPrefix(string prefix)
        {
            if (prefix.Length > 2)
                return prefix.Substring(0, 2);
            else if (prefix.Length < 2)
                return prefix.PadRight(2);
            else
                return prefix;
        }

        //? VARS
        string _content;
        string _prefix;
        Color _color;
        bool _customColor;
        bool _highlighted; // to highlight the log when browsing through logs with arrows

        //? GETTERS
        public string GetContent => _content;
        public bool GetHighlighted => _highlighted;
        string GetDisplayText => _prefix + _content;


        //? SETTERS
        public void SetHighlighted(bool highlighted)
         => _highlighted = highlighted;


        //? FUNCS
        public string GetDisplayContent()
        {
            if (_highlighted)
            {
                return TextUtilities.WrapWordWithColor(GetDisplayText, Color.yellow);
            }
            else if (_customColor)
            {
                return TextUtilities.WrapWordWithColor(GetDisplayText, _color);
            }
            else
            {
                return GetDisplayText;
            }
        }

      

    }
}