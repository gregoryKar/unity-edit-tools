using System;
using Karianakis.EditTools;
using UnityEngine;

public abstract class CommandBuilderBase
{

    //? PARAMS
    protected string _code;
    //Func<bool> _condition;
    //bool _favorite;
    //bool _nulllable;

    //? GETTERS
    internal abstract Type[] GetParameterTypes { get; }
    protected int GetParameterCount => GetParameterTypes.Length;

    internal string GetCode => _code;
    //internal bool IsFavorite => _favorite;




    //? METHODS
    protected abstract void ExecuteInternal(object[] args);

    protected bool ParameterMatching(params object[] args)
    {
        if (args.Length != GetParameterCount) return false;


        for (int i = 0; i < args.Length; i++)
        {

            Debug.LogError("arg type : " + args[i].GetType() + " expected type : " + GetParameterTypes[i]);
            if (args[i].GetType() != GetParameterTypes[i]) return false;
        }

        return true;
    }

    protected bool ParameterMatchIndexed(object arg, int index)
    {
        if (index >= GetParameterCount) return false;
        return arg.GetType() == GetParameterTypes[index];
    }



    //? EXPOSED
    internal bool TryExecuteStrings(params string[] args)
    {
        if (args.Length != GetParameterCount) return false;

        object[] convertedArgs = new object[args.Length];
        for (int i = 0; i < args.Length; i++)
        {
            bool ok = TypesUtilities.TryConvertStringToType(args[i], GetParameterTypes[i], out object converted , false);
            if (!ok) return false;
            convertedArgs[i] = converted;   
        }

        //if (_condition?.Invoke() == false) return false;


        ExecuteInternal(convertedArgs);
        return true;

    }






#if KARIANAKIS
    public bool TestTryExecuteStrings(params string[] args)
        => TryExecuteStrings(args);
#endif

}