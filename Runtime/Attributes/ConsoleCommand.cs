
using System;
using UnityEngine;

namespace Karianakis.EditTools
{




    [AttributeUsage(AttributeTargets.Method)]//, AllowMultiple = true
    public class ConsoleCommand : Attribute
    {
        public ConsoleCommand(string nickname = "")
        {
            _nickname = nickname;
        }
        string _nickname;

        public bool GetHasNickname
            => string.IsNullOrEmpty(_nickname) == false;
        public string GetCommandName => _nickname;
    }

}