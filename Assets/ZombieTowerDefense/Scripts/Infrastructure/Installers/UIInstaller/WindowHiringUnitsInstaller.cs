using UnityEngine.AddressableAssets;
using Zenject;

public class WindowHiringUnitsInstaller : Installer<AssetReference, WindowHiringUnitsInstaller>
{
    private AssetReference _windowPrefab;

    public WindowHiringUnitsInstaller(AssetReference windowPrefab)
    {
        _windowPrefab = windowPrefab;
    }

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
