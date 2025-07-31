using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyUnitSpawnerController : IInitialize, IDisposable
{
    private class UnitSpawnState
    {
        public SpawnData Data;
        public int CurrentSpawnCount;
    }

    private struct SpawnData
    {
        public UnitType UnitType;
        public int SpawnCountPerOnce;
        public int MaxCountEnemyByType;
        public float TimeBetweenSpawn;
    }

    private IConfigsProvider _configsProvider;
    private IEnemyUnitSpawner _enemyUnitSpawner;
    private ICoroutinePerformer _coroutinePerfromer;

    private EnemyUnitSpawnerControllerConfig _config;

    private Coroutine _startSpawnCoroutine;

    private Dictionary<UnitType, UnitSpawnState> _unitSpawnStates;
    private List<UnitType> _availableEnemyUnitTypes = new List<UnitType>();

    private float _timeBeforeStartSpawn;

    private bool _isCanWork;

    public EnemyUnitSpawnerController(IConfigsProvider configsProvider, IEnemyUnitSpawner enemyUnitSpawner,
        ICoroutinePerformer coroutinePerfromer)
    {
        _enemyUnitSpawner = enemyUnitSpawner;
        _configsProvider = configsProvider;
        _coroutinePerfromer = coroutinePerfromer;
    }

    public void Initialize()
    {
        Debug.Log($"EnemySpawnerController initialize");

        _config = _configsProvider.GetSingleConfig<EnemyUnitSpawnerControllerConfig>();

        if (_config == null)
        {
            Debug.LogError("EnemyUnitSpawnerControllerConfig is null. Please check your configs setup.");
            return;
        }

        _timeBeforeStartSpawn = _config.TimeBetweenStartSpawn;

        _unitSpawnStates = FillUnitSpawnStatesDict();

        _isCanWork = true;

        _startSpawnCoroutine = _coroutinePerfromer.StartRoutine(StartSpawnJob());
    }

    private Dictionary<UnitType, UnitSpawnState> FillUnitSpawnStatesDict()
    {
        Dictionary<UnitType, UnitSpawnState> unitSpawnStates = new Dictionary<UnitType, UnitSpawnState>();

        for (int i = 0; i < _config.ListEnemiesInSpawner.Count; i++)
        {
            var config = _config.ListEnemiesInSpawner[i];

            Debug.Log($"EnemySpawnerController FillDictionary config = {config}");
            Debug.Log($"EnemySpawnerController FillDictionary config.UnitType = {config.EnemyUnitType}");

            if (config == null)
            {
                Debug.LogError($"EnemySpawnerStats at index {i} is null. Please check your configs setup.");
                continue;
            }

            unitSpawnStates[config.EnemyUnitType] = new UnitSpawnState
            {
                Data = new SpawnData
                {
                    UnitType = config.EnemyUnitType,
                    SpawnCountPerOnce = config.SpawnCountPerOnce,
                    MaxCountEnemyByType = config.MaxCountEnemyByType,
                    TimeBetweenSpawn = config.TimeBetweenSpawn
                },
                CurrentSpawnCount = 0
            };

            _availableEnemyUnitTypes.Add(config.EnemyUnitType);
        }

        Debug.Log($"EnemySpawnerController FillDictionary");

        return unitSpawnStates;
    }

    public void Dispose()
    {
        _isCanWork = false;

        _coroutinePerfromer.StopRoutine(_startSpawnCoroutine);
    }

    private IEnumerator StartSpawnJob()
    {
        Debug.Log($"EnemySpawnerController StartSpawnJob");

        foreach (var type in _availableEnemyUnitTypes)
        {
            Debug.Log($"EnemySpawnerController StartSpawnJob all available unit types = {type}");

        }

        yield return new WaitForSeconds(_timeBeforeStartSpawn);

        while (_isCanWork)
        {
            UnitType unitType = UnitType.Zombie;
            //UnitType unitType = (UnitType)Random.Range(_availableEnemyUnitTypes.Count, _availableEnemyUnitTypes.Count);

            Debug.Log($"EnemySpawnerController StartSpawnJob unitType = {unitType}");

            if (_unitSpawnStates[unitType].CurrentSpawnCount >= _unitSpawnStates[unitType].Data.MaxCountEnemyByType)
            {
                yield return null;
                continue;
            }

            CreateUnitData createUnitData = new CreateUnitData(unitType,
                new Vector3(4.1f, 0f, 0f),
                new Vector3(-5f, 0f, 0f),
                Quaternion.identity,
                _unitSpawnStates[unitType].Data.TimeBetweenSpawn,
                0f);

            TryEnemySpawn(createUnitData);

            _unitSpawnStates[unitType].CurrentSpawnCount++;

            yield return new WaitForSeconds(_unitSpawnStates[unitType].Data.TimeBetweenSpawn);
        }
    }

    private void TryEnemySpawn(CreateUnitData createUnitData)
    {
        _enemyUnitSpawner.CreateUnit(createUnitData);
    }
}
