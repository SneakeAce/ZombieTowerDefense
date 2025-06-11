using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolManager
{
    private IAsyncPoolFactory _poolFactory;

    // Словарь для хранения пулов

    

    public PoolManager(IAsyncPoolFactory poolFactory, List<IPoolConfig> poolConfigs)
    {
        _poolFactory = poolFactory;

        foreach (var conf in poolConfigs)
        {
            foreach (var conf2 in conf.Configs)
            {
                
            }
            
        }

        CreatePools();
    }

    private void CreatePools()
    {



    }
}
