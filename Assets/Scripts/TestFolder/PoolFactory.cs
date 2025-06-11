using Cysharp.Threading.Tasks;
using UnityEngine;

public class PoolFactory : IAsyncPoolFactory
{
    public async UniTask<T> CreateAsync<T, TArgs>(TArgs args)
        where T : Object
        where TArgs : IFactoryArguments
    {
        if (args is not PoolCreatingArguments poolArgs)
            throw new System.ArgumentException("Invalid arguments type for PoolFactory");
        


        throw new System.Exception("CreateAsync method not implemented for PoolFactory");
    }
}
