using System.Collections.Generic;
using UnityEngine;

public interface ILibraryConfigs<T>
    where T : ScriptableObject
{
    List<T> Configs { get; }
}
