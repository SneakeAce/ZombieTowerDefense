using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField] private MainMenuSceneConfig _mainMenuSceneConfig;

    [SerializeField] private AssetReference _mainMenuCanvasPrefab;
    [SerializeField] private AssetReference _cameraPrefab;
    [SerializeField] private AssetReference _virtualCameraPrefab;

    public override void InstallBindings()
    {
        BindMainMenuControllerBinder();

        BindMainMenuFactory();

        BindSceneComponents();

        BindFirstLevelSceneLoader();

        BindMainMenuConfig();
    }

    private void BindMainMenuControllerBinder()
    {
        Container.Bind<MainMenuControllerBinder>().AsSingle();
    }

    private void BindMainMenuFactory()
    {
        Container.Bind<IMainMenuSceneAsyncObjectFactory>()
            .To<MainMenuSceneObjectFactory>()
            .AsSingle();
    }

    private void BindSceneComponents()
    {
        Container.Bind<CameraManager>()
            .AsSingle()
            .WithArguments(_cameraPrefab, _virtualCameraPrefab);


        Container.Bind<MainMenuManager>()
            .AsSingle()
            .WithArguments(_mainMenuCanvasPrefab);

        Container.BindInterfacesAndSelfTo<MainMenuSceneBootstrapper>()
            .AsSingle()
            .NonLazy();
    }

    private void BindFirstLevelSceneLoader()
    {
        Container.Bind<ISceneLoader>()
            .To<FirstLevelSceneLoader>()
            .AsSingle();
    }

    private void BindMainMenuConfig()
    {
        Container.Bind<MainMenuSceneConfig>()
           .FromInstance(_mainMenuSceneConfig)
           .AsSingle();
    }
}
