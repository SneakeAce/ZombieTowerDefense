using Zenject;

public class MainMenuControllerBinder
{
    private DiContainer _container;

    public MainMenuControllerBinder(DiContainer container)
    {
        _container = container;
    }

    public void BindMainMenuController()
    {
        _container.Bind<MainMenuController>().AsSingle().NonLazy();
        _container.Instantiate<MainMenuController>();
    }
}
