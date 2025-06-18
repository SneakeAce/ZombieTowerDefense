using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerControlledUnitsAsyncFactory : IPlayerControlledUnitsAsyncFactory
{
    public PlayerControlledUnitsAsyncFactory()
    {

    }

    public UniTask<T> CreateAsync<T, TArgs>(TArgs args)
        where T : Object
        where TArgs : IFactoryArguments
    {
        throw new System.NotImplementedException();
    }
}
