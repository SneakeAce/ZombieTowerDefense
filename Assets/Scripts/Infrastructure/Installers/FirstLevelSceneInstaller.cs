using Zenject;

public class FirstLevelSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSceneComponents();
        BindBootstrapper();
    }

    private void BindSceneComponents()
    {
        Container.Bind<ICameraManager>()
            .To<CameraManager>()
            .AsSingle();
    }

    private void BindBootstrapper()
    {
        Container.BindInterfacesAndSelfTo<FirstLevelSceneBootstrapper>()
            .AsSingle()
            .NonLazy();
    }
}
