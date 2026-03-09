


using System;

namespace Karianakis.EditTools
{

    public interface CustomCommandInterface
    {

        Type[] _parameterTypes { get; }
        bool TryExecute(object[] args);

    }
}