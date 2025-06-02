using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayerInput();

        BindAssetProvider();
    }

    private void BindAssetProvider() => Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
    private void BindPlayerInput() => Container.Bind<PlayerInput>().AsSingle();

    //6740 код выдачи
}
