using UnityEngine.AddressableAssets;
using UnityEngine;
using Zenject;

public class FirstLevelSceneUIInstaller : MonoInstaller
{
    [SerializeField] private AssetReference _windowHiringUnitPrefab;

    public override void InstallBindings()
    {
        Installer<AssetReference, WindowHiringUnitsInstaller>
            .Install(Container, _windowHiringUnitPrefab);
    }

}
