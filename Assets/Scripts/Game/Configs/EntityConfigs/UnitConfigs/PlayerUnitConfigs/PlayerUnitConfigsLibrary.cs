using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigsLibrary/UnitConfigsLibrary/PlayerUnitConfigsLibrary")]
public class PlayerUnitConfigsLibrary : ScriptableObject,
    ILibraryConfigs<PlayerUnitConfig>, IKeyedConfigs<PlayerUnitConfig, UnitType>
{
    [field: SerializeField] public List<PlayerUnitConfig> Configs { get; private set; }

    private Dictionary<UnitType, PlayerUnitConfig> _configsByType;

    private void OnEnable()
    {
        _configsByType = new Dictionary<UnitType, PlayerUnitConfig>();

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

    public PlayerUnitConfig GetByKey(UnitType key)
    {
        if (_configsByType == null)
            throw new Exception("PlayerUnitConfigsLibrary not initialized — _configsByType is null");

        if (_configsByType.TryGetValue(key, out var config))
            return config;

        Debug.LogError($"[PlayerUnitConfigsLibrary] No config found for UnitType: {key}");
        return null;
    }
}
