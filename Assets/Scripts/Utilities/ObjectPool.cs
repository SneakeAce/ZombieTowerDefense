using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<TObject> : IObjectPool<TObject>
    where TObject : MonoBehaviour
{
    private const int CountPerSpawn = 1;

    private Queue<TObject> _availableObjects;
    private HashSet<TObject> _objectsInUse;

    private TObject _prefab;
    
    private Transform _container;

    private int _countPoolObject;

    private bool _canExpandPool;

    public ObjectPool(TObject prefab, int initialSize, Transform container = null, bool canExpandPool = false)
    {
        _prefab = prefab;
        _container = container;
        _canExpandPool = canExpandPool;

        _availableObjects = new Queue<TObject>();
        _objectsInUse = new HashSet<TObject>();

        CreatePool(initialSize);
    }

    public TObject GetPoolObject()
    {
        TObject poolObject = GetObject();

        if (poolObject != null)
            return poolObject;

        if (_canExpandPool)
            return CreatePoolObject(_canExpandPool);

        return null;
    }

    public void ReturnPoolObject(TObject poolObject)
    {
        if (_objectsInUse.Contains(poolObject) == false)
            return;

        poolObject.gameObject.SetActive(false);

        _objectsInUse.Remove(poolObject);
        _availableObjects.Enqueue(poolObject);
    }

    private void CreatePool(int sizePool)
    {
        for (int i = 0; i < sizePool; i++)
            CreatePoolObject();
    }

    private TObject CreatePoolObject(bool isActiveByDefault = false)
    {
        TObject poolObject = Object.Instantiate(_prefab, _container);

        _countPoolObject += CountPerSpawn;
        poolObject.name = _prefab.name + _countPoolObject.ToString();

        poolObject.gameObject.SetActive(isActiveByDefault);
        _availableObjects.Enqueue(poolObject);

        return poolObject;
    }

    private TObject GetObject()
    {
        if (_availableObjects.Count == 0)
            return null;

        TObject poolObject = _availableObjects.Dequeue();

        _objectsInUse.Add(poolObject);

        poolObject.gameObject.SetActive(true);

        return poolObject;
    }
}
