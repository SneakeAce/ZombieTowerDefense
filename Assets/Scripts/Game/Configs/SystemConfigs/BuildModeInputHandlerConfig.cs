using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BuildModeConfigs/BulidModeInputHandlerConfig", fileName = "BulidModeInputHandlerConfig")]
public class BuildModeInputHandlerConfig : ScriptableObject
{
    [field: SerializeField] public LayerMask CellLayer { get; private set; }
    [field: SerializeField] public Color ColorAvailableCell { get; private set; }
    [field: SerializeField] public Color ColorUnavailableCell { get; private set; }
    [field: SerializeField] public Color ColorSelectedCell { get; private set; }
    
}
