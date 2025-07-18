using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class WindowHiringUnitsManager : IWindowHiringUnitsManager
{
    private AssetReference _windowPrefab;
    private Vector3 _spawnPositionCanvas = new Vector3(0, 0, 0);

    private DiContainer _container;
    private IAsyncObjectFactory _asyncObjectFactory;

    private WindowHiringUnitsView _windowHiringUnitsView;
    private Canvas _windowHiringCanvas;

    public WindowHiringUnitsManager(AssetReference windowPrefab, DiContainer container, 
        IAsyncObjectFactory asyncObjectFactory)
    {
        _windowPrefab = windowPrefab;
        _container = container;
        _asyncObjectFactory = asyncObjectFactory;
    }

    public Canvas WindowOrderingCanvas => _windowHiringCanvas;

    public async UniTask LoadPrefabAsync()
    {
        ObjectSpawnArguments objectSpawnArguments = new ObjectSpawnArguments(_windowPrefab,
            _spawnPositionCanvas, Quaternion.identity);

        _windowHiringCanvas = await _asyncObjectFactory.CreateAsync<Canvas, ObjectSpawnArguments>(objectSpawnArguments);

        if (_windowHiringCanvas == null)
            throw new NullReferenceException("WindowOrderingUnitsCanvas is Null!");

        _windowHiringUnitsView = _windowHiringCanvas.GetComponent<WindowHiringUnitsView>();

        if (_windowHiringUnitsView == null)
            throw new NullReferenceException("WindowOrderingUnitsView is Null!");

        BindWindowHiringUnitsView();
    }

    private void BindWindowHiringUnitsView()
    {
        _container.Bind<WindowHiringUnitsView>()
            .FromInstance(_windowHiringUnitsView)
            .AsSingle();
    }
}
