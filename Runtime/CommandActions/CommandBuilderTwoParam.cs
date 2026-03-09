
using System;

namespace Karianakis.EditTools
{

    public class CommandBuilderTwoParam<T1, T2> : CommandBuilderBase
    {


        private CommandBuilderTwoParam(string code , Action<T1, T2> action)
        {
            _action = action;
            _code = code;
        }
        internal static CommandBuilderTwoParam<T1, T2> Create(string code, Action<T1, T2> action)
        {
            return new CommandBuilderTwoParam<T1, T2>(code, action);
        }


        Action<T1, T2> _action;
        internal override Type[] GetParameterTypes => new Type[] { typeof(T1), typeof(T2) };

        protected override void ExecuteInternal(object[] args)
        {
            _action((T1)args[0], (T2)args[1]);
        }

        
    }
}