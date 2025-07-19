using System.Collections.Generic;
using UnityEngine;

public interface IPoolConfig<T>
    where T : ScriptableObject
{
    public int PoolSize { get; }
    public bool CanExpand { get; }
    public PoolType Type { get; }
    public Transform PoolContainer { get; }
    public List<T> Configs { get; }

}