using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface IAsyncPoolFactory
{
    PoolType PoolType { get; }

    UniTask<Dictionary<int, IObjectPool>> CreateAsync();
}
