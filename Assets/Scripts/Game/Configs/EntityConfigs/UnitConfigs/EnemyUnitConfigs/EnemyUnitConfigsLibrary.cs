using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigsLibrary/UnitConfigsLibrary/EnemyUnitConfigsLibrary")]
public class EnemyUnitConfigsLibrary : ScriptableObject,
    ILibraryConfigs<EnemyUnitConfig>, IKeyedConfigs<EnemyUnitConfig, UnitType>
{
    [field: SerializeField] public List<EnemyUnitConfig> Configs { get; private set; }

    private Dictionary<UnitType, EnemyUnitConfig> _configsByType;

    private void OnEnable()
    {
        _configsByType = new Dictionary<UnitType, EnemyUnitConfig>();

        for (int i = 0; i < Configs.Count; i++)
        {
            var currentConfig = Configs[i];

            if (currentConfig == null)
                throw new NullReferenceException("Current Config in Configs in PlayerUnitConfigsLibrary is null!");

            UnitType unitType = currentConfig.UnitMainStats.UnitType;

            if (_configsByType.ContainsKey(unitType))
                Debug.LogWarning($"Duplicate unit config for type {unitType} — overwriting.");

            _configsByType[unitType] = currentConfig;
        }
    }

    public EnemyUnitConfig GetByKey(UnitType key)
    {
        if (_configsByType == null)
            throw new Exception("PlayerUnitConfigsLibrary not initialized — _configsByType is null");

        if (_configsByType.TryGetValue(key, out var config))
            return config;

        Debug.LogError($"[PlayerUnitConfigsLibrary] No config found for UnitType: {key}");
        return null;
    }
}
