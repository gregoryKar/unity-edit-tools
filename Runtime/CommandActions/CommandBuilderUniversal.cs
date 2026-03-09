

using System;
using UnityEngine;

namespace Karianakis.EditTools
{
    public class CommandBuilderUniversal
    {

        string _code;
        readonly Delegate _action;
        readonly Type[] _paramTypes;


        internal string GetCode => _code;
        internal Type[] GetParameterTypes => _paramTypes;
        internal int GetParameterCount => _paramTypes.Length;



        CommandBuilderUniversal(string code, Delegate action, Type[] paramTypes)
        {
            _code = code;
            _action = action;
            _paramTypes = paramTypes;

            CommandTerminal.AddCommandInternal(this);
        }

        public static CommandBuilderUniversal Create(string code, Delegate action)
        {
            var paramTypes = action.Method.GetParameters();
            Type[] types = new Type[paramTypes.Length];
            for (int i = 0; i < types.Length; i++)
                types[i] = paramTypes[i].ParameterType;

            return new CommandBuilderUniversal(code, action, types);
        }

        // Overload for Action (no parameters)
        public static CommandBuilderUniversal Create(string code, Action action)
            => new CommandBuilderUniversal(code, action, Type.EmptyTypes);

        // Overload for Action<T1>
        public static CommandBuilderUniversal Create<T1>(string code, Action<T1> action)
            => new CommandBuilderUniversal(code, action, new[] { typeof(T1) });

        // Overload for Action<T1, T2>
        public static CommandBuilderUniversal Create<T1, T2>(string code, Action<T1, T2> action)
            => new CommandBuilderUniversal(code, action, new[] { typeof(T1), typeof(T2) });

        // Overload for Action<T1, T2, T3>
        public static CommandBuilderUniversal Create<T1, T2, T3>(string code, Action<T1, T2, T3> action)
            => new CommandBuilderUniversal(code, action, new[] { typeof(T1), typeof(T2), typeof(T3) });






        internal bool TryExecuteStrings(params string[] args)
        {
            if (args.Length != GetParameterCount) return false;

            object[] convertedArgs = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                bool ok = TypesUtilities.TryConvertStringToType(args[i], GetParameterTypes[i], out object converted, false);
                if (!ok) return false;
                convertedArgs[i] = converted;
            }

            try
            {
                _action.DynamicInvoke(convertedArgs);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return false;
            }



        }


    }
}