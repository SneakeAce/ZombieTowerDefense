using UnityEngine;

public interface IFactory
{
    T CreateObject<T, TArgs>(TArgs args) 
        where T : Object
        where TArgs : IFactoryArguments;
}
