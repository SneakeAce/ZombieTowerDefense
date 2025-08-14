using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface IPoolsFactoryAsync
{
    PoolType PoolType { get; }

    UniTask<Dictionary<int, IObjectPool>> CreateAsync();
}
