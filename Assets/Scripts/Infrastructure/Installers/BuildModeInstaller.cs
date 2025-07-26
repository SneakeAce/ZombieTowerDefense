using Zenject;

public class BuildModeInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindGridManager();

        BindBuildModeComponents();
    }

    private void BindGridManager()
    {
        Container.Bind<IGridCellFactory>().To<GridCellFactory>().AsSingle();

        Container.Bind<IGridManager>().To<GridManager>().AsSingle();
    }

    private void BindBuildModeComponents()
    {
        Container.Bind<BuildModeInputHandler>().AsSingle();

        Container.Bind<IBuildModeController>().To<BuildModeController>().AsSingle().NonLazy();
    }
}
