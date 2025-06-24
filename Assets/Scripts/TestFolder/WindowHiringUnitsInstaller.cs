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
        Container.Bind<IWindowHiringUnitsManager>()
            .To<WindowHiringUnitsManager>()
            .AsSingle()
            .WithArguments(_windowPrefab);
    }

    private void BindWindowHiringUnitsController()
    {
        Container.Bind<IWindowHiringUnitsController>()
            .To<WindowHiringUnitsController>()
            .AsSingle();
    }
}
