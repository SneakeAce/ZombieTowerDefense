using UnityEngine;

public interface IKeyedConfigs<TConfig, in TKey>
    where TConfig : ScriptableObject
{
    TConfig GetByKey(TKey key);
}
