using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField] private AssetReference _mainMenuCanvasPrefab; 

    public override void InstallBindings()
    {
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
            .AsSingle();

        Container.Bind<IMainMenuManager>()
            .To<MainMenuManager>()
            .AsSingle()
            .WithArguments(_mainMenuCanvasPrefab);

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
}
