using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _preformerPrefab;
    [SerializeField] private AssetLabelReference _configsLibraryLabel;
    [SerializeField] private AssetLabelReference _singleConfigsLabel;
    [SerializeField] private AssetLabelReference _sceneConfigsLabel;

    public override void InstallBindings()
    {
        BindPlayerInput();

        BindGlobalServices();

        BindCoroutinePerformer();

        BindGlobalBootstrapper();

        BindMainMenuBootstrapper();
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
            .WithArguments(_configsLibraryLabel, _singleConfigsLabel, _sceneConfigsLabel);

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

    private void BindMainMenuBootstrapper()
    {
        Container.BindInterfacesAndSelfTo<MainMenuSceneBootstrapper>()
            .AsSingle();
    }
}
