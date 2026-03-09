using System;

namespace Karianakis.EditTools
{

    public class CommandBuilderThreeParam<T1, T2, T3> : CommandBuilderBase
    {


        private CommandBuilderThreeParam(string code , Action<T1, T2, T3> action)
        {
            _action = action;
            _code = code;
        }
        internal static CommandBuilderThreeParam<T1, T2, T3> Create(string code, Action<T1, T2, T3> action)
        {
            return new CommandBuilderThreeParam<T1, T2, T3>(code, action);
        }


        Action<T1, T2, T3> _action;
        internal override Type[] GetParameterTypes => new Type[] { typeof(T1), typeof(T2), typeof(T3) };

        protected override void ExecuteInternal(object[] args)
        {
            _action((T1)args[0], (T2)args[1], (T3)args[2]);
        }

        
    }
}