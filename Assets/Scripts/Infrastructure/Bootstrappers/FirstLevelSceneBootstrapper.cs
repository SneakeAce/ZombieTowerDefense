using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FirstLevelSceneBootstrapper : IInitializable
{
    private IPoolManager _poolManager;
    private ICameraManager _cameraManager;

    private IGridManager _gridManager;
    private IBuildModeController _buildModeController;

    private IWindowUnitsHiringManager _windowUnitsHiringManager;
    private IWindowUnitsHiringController _windowHiringUnitsController;

    private IUnitHiringController _unitHiringController;
    private IUnitHiringButtonsController _unitHiringButtonsController;

    private IInitializer _initializer;

    private List<IInitialize> _initializeList = new List<IInitialize>();

    private EnemyUnitSpawnerController _enemySpawnerController;

    public FirstLevelSceneBootstrapper(IPoolManager poolManager, ICameraManager cameraManager, IGridManager gridManager,
        IWindowUnitsHiringManager windowHiringUnitsManager, IUnitHiringButtonsController buttonsController,
        IWindowUnitsHiringController windowHiringUnitsController, IBuildModeController buildModeController,
        IUnitHiringController unitHiringController, IInitializer initializer, EnemyUnitSpawnerController enemySpawnerController)
    {
        _poolManager = poolManager;

        _cameraManager = cameraManager;

        _gridManager = gridManager;
        _windowUnitsHiringManager = windowHiringUnitsManager;
        _unitHiringButtonsController = buttonsController;
        _windowHiringUnitsController = windowHiringUnitsController;
        _buildModeController = buildModeController;
        _unitHiringController = unitHiringController;

        _initializer = initializer;

        _enemySpawnerController = enemySpawnerController;

        FillInitializeList();
    }

    public void Initialize()
    {
        LoadManagersAsync().Forget();
    }

    private void FillInitializeList()
    {
        _initializeList.Add(_gridManager);
        _initializeList.Add(_buildModeController);
        _initializeList.Add(_windowHiringUnitsController);
        _initializeList.Add(_unitHiringButtonsController);
        _initializeList.Add(_unitHiringController);
        _initializeList.Add(_enemySpawnerController);
    }

    private async UniTask LoadManagersAsync()
    {
        await _poolManager.CreatePoolsAsync();

        _windowUnitsHiringManager.BindWindowDone += _unitHiringButtonsController.GetView;

        await _cameraManager.LoadAndCreateCameraAsync();
        await _windowUnitsHiringManager.LoadPrefabAsync();

        _initializer.Initialize(_initializeList);

        Debug.Log("FirstLevelSceneBootstrapper LoadManagersAsync end");
    }
}
