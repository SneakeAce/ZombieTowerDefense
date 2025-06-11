using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField] private MainMenuSceneConfig _mainMenuSceneConfig;

    public override void InstallBindings()
    {
        BindMainMenuControllerBinder();

        BindSceneObjectFactory();

        BindSceneComponents();

        BindFirstLevelSceneLoader();

        BindMainMenuConfig();
    }

    private void BindMainMenuControllerBinder()
    {
        Container.Bind<MainMenuControllerBinder>().AsSingle();
    }

    private void BindSceneObjectFactory()
    {
        Container.Bind<IAsyncObjectFactory>()
            .To<SceneObjectFactory>()
            .AsSingle();
    }

    private void BindSceneComponents()
    {
        Container.Bind<CameraManager>()
            .AsSingle()
            .WithArguments(_mainMenuSceneConfig.SpawnCameraData);


        Container.Bind<MainMenuManager>()
            .AsSingle()
            .WithArguments(_mainMenuSceneConfig.MainMenuCanvasPrefab);

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
