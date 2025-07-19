using UnityEngine;

[CreateAssetMenu(menuName = "Configs/GridManagerConfig", fileName = "GridManagerConfig")]
public class GridManagerConfig : ScriptableObject
{
    [field: SerializeField] public GridManagerStats GridManagerStats { get; private set; }
}
