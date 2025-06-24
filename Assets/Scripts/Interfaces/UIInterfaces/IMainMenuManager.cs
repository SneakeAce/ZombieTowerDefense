using UnityEngine;

public interface IMainMenuManager : IUIManager
{
    public MainMenuView MainMenuView { get; }
    public Canvas MainMenuCanvas { get; }
}
