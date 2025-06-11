using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IAsyncFactory
{
    UniTask<T> CreateAsync<T, TArgs>(TArgs args) 
        where T : Object
        where TArgs : IFactoryArguments;
}

