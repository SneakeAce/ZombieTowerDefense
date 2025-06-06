using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

public class FirstLevelInstaller : MonoInstaller
{
    [SerializeField] private UnitConfig _unitConfig;

    public override void InstallBindings()
    {
        BindUnitConfig();

        BindServices();

        CreateAndBindTestCharacter();
    }


    private void BindServices()
    {
        Container.Bind<IUnitHealth>().To<UnitHealth>().AsTransient();

        Container.Bind<ICommandInvoker>().To<CommandInvoker>().AsSingle();

        Container.Bind<UnitMoveTargetSelector>().AsSingle();

        Container.Bind<SelectUnit>().AsSingle().NonLazy();
    }

    /// <summary>
    /// Test Methods
    /// </summary>
    private void BindUnitConfig() => Container.Bind<UnitConfig>().FromInstance(_unitConfig).AsTransient();

    private void CreateAndBindTestCharacter()
    {
        Unit unit = Container
            .InstantiatePrefabForComponent<Unit>(
            _unitConfig.Prefab,
            new Vector3(0, 0, 0),
        Quaternion.identity, null);

        Container.BindInterfacesAndSelfTo<Unit>().FromInstance(unit).AsSingle();
    }

}
