using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenuSceneConfig _mainMenuSceneConfig;
    [SerializeField] private MainMenuView _mainMenuView;

    public override void InstallBindings()
    {
        BindMainMenuSceneConfig();

        BindFirstLevelSceneLoader();

        BindMainMenuView();

        BindMainMenuController();
    }

    private void BindMainMenuSceneConfig()
    {
        Container.Bind<MainMenuSceneConfig>()
           .FromInstance(_mainMenuSceneConfig)
           .AsSingle();
    }

    private void BindMainMenuController()
    {
        Container.Bind<MainMenuController>()
            .AsSingle()
            .NonLazy();
    }

    private void BindFirstLevelSceneLoader()
    {
        Container.Bind<ISceneLoader>()
            .To<FirstLevelSceneLoader>()
            .AsSingle();
    }

    private void BindMainMenuView()
    {
        Container.Bind<MainMenuView>()
            .FromInstance(_mainMenuView)
            .AsSingle();
    }
}
