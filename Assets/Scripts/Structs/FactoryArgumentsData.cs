using UnityEngine;
using UnityEngine.AddressableAssets;

public struct ObjectSpawnArguments : IFactoryArguments
{
    public ObjectSpawnArguments(AssetReference assetReference, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        AssetReference = assetReference;
        SpawnPosition = spawnPosition;
        SpawnRotation = spawnRotation;
    }

    public AssetReference AssetReference { get; }
    public Vector3 SpawnPosition { get; }
    public Quaternion SpawnRotation { get; }
}

public struct UnitSpawnArguments : IFactoryArguments
{
    public UnitSpawnArguments(IObjectPool pool, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        Pool = pool;
        SpawnPosition = spawnPosition;
        SpawnRotation = spawnRotation;
    }

    public IObjectPool Pool { get; }
    public Vector3 SpawnPosition { get; }
    public Quaternion SpawnRotation { get; }
}


public struct PoolCreatingArguments : IFactoryArguments
{
    public PoolCreatingArguments(int poolSize, bool canExpandPool, Transform container)
    {
        PoolSize = poolSize;
        CanExpandPool = canExpandPool;
        Container = container;
    }

    public int PoolSize { get; }
    public bool CanExpandPool { get; } 
    public Transform Container { get; }
}
