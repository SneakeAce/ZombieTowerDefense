using UnityEngine;
using Zenject;

public class BuildModeInstaller : MonoInstaller
{
    [SerializeField] private BuildModeInputHandlerConfig _configHandler;

    public override void InstallBindings()
    {
        BindBuildModeComponents();
    }

    private void BindBuildModeComponents()
    {
        Container.Bind<BuildModeInputHandlerConfig>().FromInstance(_configHandler).AsSingle();

        Container.Bind<BuildModeInputHandler>().AsSingle();

        Container.Bind<IBuildModeController>().To<BuildModeController>().AsSingle().NonLazy();
    }

}
