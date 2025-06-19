using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _preformerPrefab;

    public override void InstallBindings()
    {
        BindPlayerInput();

        BindAssetProvider();

        BindCoroutinePerformer();
    }

    private void BindAssetProvider() => Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
    private void BindPlayerInput() => Container.Bind<PlayerInput>().AsSingle();

    private void BindCoroutinePerformer()
    {
        CoroutinePerformer performer = Instantiate(_preformerPrefab);

        Container.Bind<ICoroutinePerformer>()
            .To<CoroutinePerformer>()
            .FromInstance(performer)
            .AsSingle();
    }
}
