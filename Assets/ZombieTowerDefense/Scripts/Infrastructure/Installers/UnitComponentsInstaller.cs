using Zenject;

public class UnitComponentsInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        BindHealthComponent();

        BindWeaponComponents();
    }

    private void BindHealthComponent() => 
        Container.Bind<IUnitHealth>()
            .To<UnitHealth>()
            .AsTransient();

    private void BindWeaponComponents()
    {
        Container.Bind<SearchTargetSystem>().AsTransient();
        Container.Bind<RotateToTarget>().AsTransient();
        Container.Bind<WeaponAttackController>().AsTransient();
    }
}
