using UnityEngine;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    [SerializeField] private GameObject _containerRootPrefab;

    public override void InstallBindings()
    {
        BindContainerCreator();

        BindServices();

        BindPoolManagerAndAsyncPoolFactories();

        BindSceneObjectFactory();
    }

    private void BindContainerCreator()
    {
        GameObject rootContainer = Container.InstantiatePrefab(_containerRootPrefab);

        Container.Bind<IContainersCreator>()
            .To<ContainersCreator>()
            .AsSingle()
            .WithArguments(rootContainer);
    }

    private void BindServices()
    {
        Container.Bind<ICommandInvoker>()
            .To<CommandInvoker>()
            .AsSingle();

        Container.Bind<UnitMoveTargetSelector>().AsSingle();

        Container.Bind<SelectUnit>()
            .AsSingle()
            .NonLazy();
    }

    private void BindSceneObjectFactory()
    {
        Container.Bind<IAsyncObjectFactory>()
            .To<SceneObjectFactory>()
            .AsSingle();
    }

    private void BindPoolManagerAndAsyncPoolFactories()
    {
        Container.Bind<IAsyncPoolFactory>()
            .To<PlayerUnitPoolsFactory>()
            .AsSingle();

        Container.Bind<IPoolManager>().To<PoolManager>().AsSingle();
    }

}
