using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _preformerPrefab;
    [SerializeField] private AssetLabelReference _configsLibraryLabel;

    public override void InstallBindings()
    {
        BindPlayerInput();

        BindAssetProvider();

        BindInitializer();

        BindConfigsProvider();

        BindCoroutinePerformer();

        BindGlobalBootstrapper();
    }

    private void BindAssetProvider() => Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();

    private void BindInitializer() => Container.Bind<IInitializer>().To<Initializer>().AsSingle();

    private void BindPlayerInput()
    {
        Container.Bind<PlayerInput>().AsSingle();

        Container.Bind<PlayerInputManager>().AsSingle().NonLazy();
    }

    private void BindCoroutinePerformer()
    {
        CoroutinePerformer performer = Instantiate(_preformerPrefab);

        Container.Bind<ICoroutinePerformer>()
            .To<CoroutinePerformer>()
            .FromInstance(performer)
            .AsSingle();
    }

    private void BindConfigsProvider()
    {
        Container.Bind<ConfigsLoader>()
            .AsSingle()
            .WithArguments(_configsLibraryLabel);

        Container.Bind<IConfigsProvider>()
            .To<ConfigsProvider>()
            .AsSingle();
    }

    private void BindGlobalBootstrapper()
    {
        Container.BindInterfacesAndSelfTo<GlobalBootstrapper>()
        .AsSingle()
        .NonLazy();
    }
}
