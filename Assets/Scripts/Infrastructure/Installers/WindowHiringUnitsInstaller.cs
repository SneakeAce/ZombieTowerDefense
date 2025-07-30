using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class WindowHiringUnitsInstaller : MonoInstaller
{
    [SerializeField] private AssetReference _windowPrefab;

    public override void InstallBindings()
    {
        BindWindowHiringUnitsManager();

        BindWindowHiringUnitsController();
    }

    private void BindWindowHiringUnitsManager()
    {
        Container.Bind<IWindowUnitsHiringManager>()
            .To<WindowUnitsHiringManager>()
            .AsSingle()
            .WithArguments(_windowPrefab);
    }

    private void BindWindowHiringUnitsController()
    {
        Container.Bind<IWindowUnitsHiringController>()
            .To<WindowUnitsHiringController>()
            .AsSingle();
    }
}
