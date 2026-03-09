

using System;

namespace Karianakis.EditTools
{

    public class CommandBuilderOneParam<T1> : CommandBuilderBase
    {


        private CommandBuilderOneParam(string code , Action<T1> action)
        {
            _action = action;
            _code = code;
        }
        internal static CommandBuilderOneParam<T1> Create(string code, Action<T1> action)
        {
            return new CommandBuilderOneParam<T1>(code, action);
        }

        Action<T1> _action;
        internal override Type[] GetParameterTypes => new Type[] { typeof(T1) };

        public Type[] TestGetParameters => new Type[] { typeof(T1) };


        protected override void ExecuteInternal(object[] args)
        {
            _action.Invoke((T1)args[0]);
        }
    }
}