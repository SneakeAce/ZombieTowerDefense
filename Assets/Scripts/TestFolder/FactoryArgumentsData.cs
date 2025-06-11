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

public struct PoolCreatingArguments : IFactoryArguments
{
    public PoolCreatingArguments(AssetReference assetReference, UnitType unitType)
    {
        AssetReference = assetReference;
        UnitType = unitType;
    }

    public AssetReference AssetReference { get; }
    public UnitType UnitType { get; }
}
