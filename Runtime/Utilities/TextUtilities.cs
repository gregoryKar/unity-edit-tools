




using TMPro;
using UnityEngine;

namespace Karianakis.EditTools
{

    public static class TextUtilities
    {
        public static void StyleAfterFirstLine(TMP_Text text, Color color)//, int sizePercent
        {
            if (text == null) return;

            // Force layout calculation
            text.ForceMeshUpdate();

            var textInfo = text.textInfo;

            if (textInfo.lineCount <= 1)
                return; // Nothing to modify

            // Get index where second line starts
            int secondLineStartIndex = textInfo.lineInfo[1].firstCharacterIndex;

            string original = text.text;

            if (secondLineStartIndex >= original.Length)
                return;

            string firstPart = original.Substring(0, secondLineStartIndex);
            string secondPart = original.Substring(secondLineStartIndex);

            string hexColor = ColorUtility.ToHtmlStringRGB(color);

            text.text =
                firstPart +
                $"<color=#{hexColor}>" +
                secondPart +
                "</color>";

            text.ForceMeshUpdate();

            ////<size={sizePercent}%></size>"


        }


        public static string WrapWordWithColor(string word, Color col)
                  => $"<color=#{ColorUtility.ToHtmlStringRGB(col)}>{word}</color>";

        public static string GetTmpColorInitialiseCode(Color color)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(color);
            return $"<color=#{hexColor}>";
        }



        public static char KeyCodeToChar(KeyCode key, bool shiftPressed)
        {
            // Map KeyCode to char, with basic support for numbers, symbols, and Shift

            // Letters
            if (key >= KeyCode.A && key <= KeyCode.Z)
                return (char)((shiftPressed ? 'A' : 'a') + (key - KeyCode.A));
            // Numbers (top row)
            if (key >= KeyCode.Alpha0 && key <= KeyCode.Alpha9)
            {
                if (!shiftPressed)
                    return (char)('0' + (key - KeyCode.Alpha0));
                // Shifted symbols for numbers
                char[] shifted = { ')', '!', '@', '#', '$', '%', '^', '&', '*', '(' };
                return shifted[key - KeyCode.Alpha0];
            }
            // Numpad
            if (key >= KeyCode.Keypad0 && key <= KeyCode.Keypad9)
                return (char)('0' + (key - KeyCode.Keypad0));
            // Space
            if (key == KeyCode.Space) return ' ';

            if(key == KeyCode.LeftArrow) return '<';
            if (key == KeyCode.RightArrow) return '>';
            if (key == KeyCode.UpArrow) return '^';
            if (key == KeyCode.DownArrow) return 'v';

            // Symbols (US QWERTY)
            if (!shiftPressed)
            {
                if (key == KeyCode.Minus) return '-';
                if (key == KeyCode.Equals) return '=';
                if (key == KeyCode.LeftBracket) return '[';
                if (key == KeyCode.RightBracket) return ']';
                if (key == KeyCode.Backslash) return '\\';
                if (key == KeyCode.Semicolon) return ';';
                if (key == KeyCode.Quote) return '\'';
                if (key == KeyCode.Comma) return ',';
                if (key == KeyCode.Period) return '.';
                if (key == KeyCode.Slash) return '/';
                if (key == KeyCode.BackQuote) return '`';
            }
            else // Shifted symbols
            {
                if (key == KeyCode.Minus) return '_';
                if (key == KeyCode.Equals) return '+';
                if (key == KeyCode.LeftBracket) return '{';
                if (key == KeyCode.RightBracket) return '}';
                if (key == KeyCode.Backslash) return '|';
                if (key == KeyCode.Semicolon) return ':';
                if (key == KeyCode.Quote) return '"';
                if (key == KeyCode.Comma) return '<';
                if (key == KeyCode.Period) return '>';
                if (key == KeyCode.Slash) return '?';
                if (key == KeyCode.BackQuote) return '~';
            }
            // No char mapping
            return '\0';
        }


        public static bool IsValidPrintableChar(char c)
        {
            return char.IsLetterOrDigit(c) || c == '.' || c == '-';
        }


    }
}