using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _preformerPrefab;
    [SerializeField] private AssetLabelReference _configsLibraryLabel;
    [SerializeField] private AssetLabelReference _objectConfigsLabel;
    [SerializeField] private AssetLabelReference _sceneConfigsLabel;

    public override void InstallBindings()
    {
        BindPlayerInput();

        BindGlobalServices();

        BindCoroutinePerformer();

        BindGlobalBootstrapper();
    }

    private void BindPlayerInput()
    {
        Container.Bind<PlayerInput>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerInputManager>().AsSingle();
    }

    private void BindGlobalServices()
    {
        Container.Bind<IInitializer>()
            .To<Initializer>()
            .AsSingle();

        Container.Bind<IAssetProvider>()
            .To<AssetProvider>()
            .AsSingle();

        Container.Bind<ConfigsLoader>()
            .AsSingle()
            .WithArguments(_configsLibraryLabel, _objectConfigsLabel, _sceneConfigsLabel);

        Container.Bind<IConfigsProvider>()
            .To<ConfigsProvider>()
            .AsSingle();
    }

    private void BindCoroutinePerformer()
    {
        CoroutinePerformer performer = Instantiate(_preformerPrefab);

        Container.Bind<ICoroutinePerformer>()
            .To<CoroutinePerformer>()
            .FromInstance(performer)
            .AsSingle();
    }

    private void BindGlobalBootstrapper()
    {
        Container.BindInterfacesAndSelfTo<GlobalBootstrapper>()
        .AsSingle()
        .NonLazy();
    }
}
