using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startLevelButton;
    
    public Button StartLevelButton { get => _startLevelButton; }
}
