using UnityEngine;
using Zenject;

public class FirstLevelSceneInstaller : MonoInstaller
{
    [SerializeField] private GameObject _containerRootPrefab;

    private GameObject _containerRoot;

    public override void InstallBindings()
    {
        CreateContainerRoot();

        BindContainerCreator();

        BindSceneObjectFactory();

        BindSceneComponents();
    }

    private void CreateContainerRoot()
    {
        _containerRoot = Container.InstantiatePrefab(_containerRootPrefab);
    }

    private void BindContainerCreator()
    {
        Container.Bind<IContainersCreator>()
            .To<ContainersCreator>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("ContainersCreator")
            .AsSingle()
            .WithArguments(_containerRoot);
    }

    private void BindSceneObjectFactory()
    {
        Container.Bind<IAsyncObjectFactory>()
            .To<SceneObjectFactory>()
            .AsSingle();
    }

    private void BindSceneComponents()
    {
        Container.Bind<ICameraManager>()
            .To<CameraManager>()
            .AsSingle();
    }
}
