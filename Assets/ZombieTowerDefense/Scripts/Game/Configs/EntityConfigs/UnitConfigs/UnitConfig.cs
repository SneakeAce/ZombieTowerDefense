using UnityEngine;

public class UnitConfig : ScriptableObject
{
    [field: SerializeField] public UnitMainStats UnitMainStats { get; private set; }

}
