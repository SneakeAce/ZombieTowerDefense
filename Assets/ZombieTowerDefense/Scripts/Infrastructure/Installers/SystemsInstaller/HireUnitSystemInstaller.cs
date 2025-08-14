using Zenject;

public class HireUnitSystemInstaller : Installer
{
    public override void InstallBindings()
    {
        BindUnitHireSystem();
    }

    private void BindUnitHireSystem()
    {
        Container.Bind<IUnitHiringButtonsController>()
            .To<UnitHiringButtonsController>()
            .AsSingle();

        Container.Bind<IUnitHiringController>()
            .To<UnitHiringController>()
            .AsSingle();
    }
}
