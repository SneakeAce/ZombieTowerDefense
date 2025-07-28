using UnityEngine;
using Zenject;

public class SystemsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayerUnitHireSystem();

    }

    private void BindPlayerUnitHireSystem()
    {
        Container.Bind<IUnitHiringButtonsController>()
            .To<UnitHiringButtonsController>()
            .AsSingle();

        Container.Bind<IUnitHiringController>()
            .To<UnitHiringController>()
            .AsSingle();
    }

    private void BindBuildModeSystem()
    {
        Container.Bind<IGridCellFactory>().To<GridCellFactory>().AsSingle();

        Container.Bind<IGridManager>().To<GridManager>().AsSingle();

        Container.Bind<BuildModeInputHandler>().AsSingle();

        Container.Bind<IBuildModeController>().To<BuildModeController>().AsSingle();
    }
}
