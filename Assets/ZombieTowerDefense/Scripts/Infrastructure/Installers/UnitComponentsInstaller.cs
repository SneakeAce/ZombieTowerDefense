using Zenject;

public class UnitComponentsInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        BindHealthComponent();
    }

    private void BindHealthComponent() => 
        Container.Bind<IUnitHealth>()
            .To<UnitHealth>()
            .AsTransient();
}
