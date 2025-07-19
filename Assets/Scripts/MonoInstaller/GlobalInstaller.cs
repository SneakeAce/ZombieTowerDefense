using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _preformerPrefab;

    public override void InstallBindings()
    {
        BindPlayerInput();

        BindAssetProvider();

        BindInitializer();

        BindCoroutinePerformer();
    }

    private void BindAssetProvider() => Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();

    private void BindInitializer() => Container.Bind<IInitializer>().To<Initializer>().AsSingle();

    private void BindPlayerInput()
    {
        Container.Bind<PlayerInput>().AsSingle();

        Container.Bind<PlayerInputManager>().AsSingle().NonLazy();
    }

    private void BindCoroutinePerformer()
    {
        CoroutinePerformer performer = Instantiate(_preformerPrefab);

        Container.Bind<ICoroutinePerformer>()
            .To<CoroutinePerformer>()
            .FromInstance(performer)
            .AsSingle();
    }
}
