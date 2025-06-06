using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfig")]
public class UnitConfig : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }

    [field: SerializeField] public MoveStats MoveStats { get; private set; }
    [field: SerializeField] public HealthStats HealthStats { get; private set; }
    [field: SerializeField] public AttackStats AttackStats { get; private set; }
}
