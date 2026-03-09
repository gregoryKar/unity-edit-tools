



using System;

namespace Karianakis.EditTools
{

    public class CustomCommand
    {

        public static CommandBuilderNoParam Simple(string code , Action action)
        {
            return CommandBuilderNoParam.Create(code , action);
        }

        public static CommandBuilderOneParam<T1> OneParam<T1>(string code, Action<T1> action)
        {
            return CommandBuilderOneParam<T1>.Create(code, action);
        }

        public static CommandBuilderTwoParam<T1, T2> TwoParam<T1, T2>(string code, Action<T1,T2> action)
        {
            return CommandBuilderTwoParam<T1, T2>.Create(code, action);
        }

         public static CommandBuilderThreeParam<T1, T2, T3> ThreeParam<T1, T2 , T3>(string code, Action<T1,T2,T3> action)
        {
            return CommandBuilderThreeParam<T1, T2 , T3>.Create(code, action);
        }



    }

}