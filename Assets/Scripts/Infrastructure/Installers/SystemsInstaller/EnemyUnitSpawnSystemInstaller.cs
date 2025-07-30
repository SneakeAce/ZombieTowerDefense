using Zenject;

public class EnemyUnitSpawnSystemInstaller : Installer
{
    public override void InstallBindings()
    {
        BindEnemyUnitSpawnSystem();
    }

    private void BindEnemyUnitSpawnSystem()
    {
        Container.Bind<IEnemyUnitSpawner>()
            .To<EnemyUnitSpawner>()
            .AsSingle();

        Container.Bind<IEnemyUnitSpawnerManager>()
            .To<EnemyUnitSpawnerManager>()
            .AsSingle();
    }
}
