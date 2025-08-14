using Zenject;

public class SystemsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Install<BuildModeSystemInstaller>();

        Container.Install<HireUnitSystemInstaller>();

        Container.Install<UnitSpawnSystemsInstaller>();
    }
}
