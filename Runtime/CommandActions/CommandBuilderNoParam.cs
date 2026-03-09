
using System;

namespace Karianakis.EditTools
{

    public class CommandBuilderNoParam : CommandBuilderBase
    {


        private CommandBuilderNoParam(string code , Action action)
        {
            _action = action;
            _code = code;
        }
        internal static CommandBuilderNoParam Create(string code, Action action)
        {
            return new CommandBuilderNoParam(code, action);
        }

        Action _action;
        internal override Type[] GetParameterTypes => new Type[0];

        protected override void ExecuteInternal(object[] args)
        {
            _action.Invoke();
        }



    }
}