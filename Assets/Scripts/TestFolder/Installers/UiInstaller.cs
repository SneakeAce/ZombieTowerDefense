using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class UiInstaller : MonoInstaller
{
    [SerializeField] private AssetReference _windowUnitsHiringPrefab;

    public override void InstallBindings()
    {
        BindWindowUnitsHiring();
    }

    private void BindWindowUnitsHiring()
    {
        Container.Bind<IWindowUnitsHiringManager>()
            .To<WindowUnitsHiringManager>()
            .AsSingle()
            .WithArguments(_windowUnitsHiringPrefab);

        Container.Bind<IWindowUnitsHiringController>()
            .To<WindowUnitsHiringController>()
            .AsSingle();
    }
}
