using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfigs/HiringConfigs/UnitHiringConfig")]
public class UnitHiringConfig : ScriptableObject
{
    [SerializeField] public UnitHiringStats UnitHiringStats { get; private set; }    
}
