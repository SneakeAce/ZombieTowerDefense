using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField] private MainMenuSceneConfig _mainMenuSceneConfig;

    public override void InstallBindings()
    {
        BindMainMenuConfig();

        BindSceneObjectFactory();

        BindSceneComponents();

        BindFirstLevelSceneLoader();
    }

    private void BindSceneObjectFactory()
    {
        Container.Bind<IAsyncObjectFactory>()
            .To<SceneObjectFactory>()
            .AsSingle();
    }

    private void BindSceneComponents()
    {
        Container.Bind<ICameraManager>()
            .To<CameraManager>()
            .AsSingle()
            .WithArguments(_mainMenuSceneConfig.SpawnCameraData);

        Container.Bind<IMainMenuManager>()
            .To<MainMenuManager>()
            .AsSingle()
            .WithArguments(_mainMenuSceneConfig.MainMenuCanvasPrefab);

        Container.Bind<IMainMenuController>()
            .To<MainMenuController>()
            .AsSingle();
            
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
