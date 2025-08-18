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
    public UnitSpawnArguments(IObjectPool pool, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType)
    {
        Pool = pool;
        SpawnPosition = spawnPosition;
        SpawnRotation = spawnRotation;
        UnitPoolType = poolType;
    }

    public IObjectPool Pool { get; }
    public Vector3 SpawnPosition { get; }
    public Quaternion SpawnRotation { get; }
    public PoolType UnitPoolType { get; }
}

public struct WeaponSpawnArguments : IFactoryArguments
{
    public WeaponSpawnArguments(GameObject parent, WeaponConfig config, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        Parent = parent;
        Config = config;
        SpawnPosition = spawnPosition;
        SpawnRotation = spawnRotation;
    }

    public GameObject Parent { get; }
    public WeaponConfig Config { get; }
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

public struct GridCellCreatingArguments : IFactoryArguments
{
    public GridCellCreatingArguments(Vector3 position, Quaternion rotation, Transform parent, GridCell gridCellPrefab,
        LayerMask groundLayer, LayerMask obstacleLayer)
    {
        Position = position;
        Rotation = rotation;
        Parent = parent;
        GridCellPrefab = gridCellPrefab;
        GroundLayer = groundLayer;
        ObstacleLayer = obstacleLayer;
    }

    public Vector3 Position { get; }
    public Quaternion Rotation { get; }
    public Transform Parent { get; }
    public GridCell GridCellPrefab { get; }
    public LayerMask GroundLayer { get; }
    public LayerMask ObstacleLayer { get; }
}
