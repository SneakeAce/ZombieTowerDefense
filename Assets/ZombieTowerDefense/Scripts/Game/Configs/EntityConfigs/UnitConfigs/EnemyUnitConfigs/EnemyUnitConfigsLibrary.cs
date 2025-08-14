using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigsLibrary/UnitConfigsLibrary/EnemyUnitConfigsLibrary")]
public class EnemyUnitConfigsLibrary : ScriptableObject,
    ILibraryConfigs<EnemyUnitConfig>, IKeyedConfigs<EnemyUnitConfig, EnemyUnitType>
{
    [field: SerializeField] public List<EnemyUnitConfig> Configs { get; private set; }

    private Dictionary<EnemyUnitType, EnemyUnitConfig> _configsByType;

    private void OnEnable()
    {
        _configsByType = new Dictionary<EnemyUnitType, EnemyUnitConfig>();

        for (int i = 0; i < Configs.Count; i++)
        {
            var currentConfig = Configs[i];

            if (currentConfig == null)
                throw new NullReferenceException("Current Config in Configs in PlayerUnitConfigsLibrary is null!");

            EnemyUnitType unitType = currentConfig.UnitType;

            if (_configsByType.ContainsKey(unitType))
                Debug.LogWarning($"Duplicate unit config for type {unitType} — overwriting.");

            _configsByType[unitType] = currentConfig;
        }
    }

    public EnemyUnitConfig GetByKey(EnemyUnitType key)
    {
        if (_configsByType == null)
            throw new Exception("PlayerUnitConfigsLibrary not initialized — _configsByType is null");

        if (_configsByType.TryGetValue(key, out var config))
            return config;

        Debug.LogError($"[PlayerUnitConfigsLibrary] No config found for UnitType: {key}");
        return null;
    }
}
