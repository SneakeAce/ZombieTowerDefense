using UnityEngine;
using Zenject;

public class FirstLevelSceneInstaller : MonoInstaller
{
    [SerializeField] private FirstLevelSceneConfig _firstLevelSceneConfig;

    public override void InstallBindings()
    {
        BindSceneObjectFactory();

        BindSceneComponents();

        BindFirstLevelSceneConfig();
    }

    private void BindSceneObjectFactory()
    {
        Container.Bind<IAsyncObjectFactory>()
            .To<SceneObjectFactory>()
            .AsSingle();
    }

    private void BindSceneComponents()
    {
        Container.Bind<CameraManager>()
            .AsSingle()
            .WithArguments(_firstLevelSceneConfig.SpawnCameraData);

        Container.BindInterfacesAndSelfTo<FirstLevelSceneBootstrapper>()
            .AsSingle()
            .NonLazy();
    }

    private void BindFirstLevelSceneConfig()
    {
        Container.Bind<FirstLevelSceneConfig>()
           .FromInstance(_firstLevelSceneConfig)
           .AsSingle();
    }
}
