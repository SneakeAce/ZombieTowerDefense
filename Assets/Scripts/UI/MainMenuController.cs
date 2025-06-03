public class MainMenuController
{
    private MainMenuView _mainMenuView;
    private MainMenuSceneConfig _mainMenuSceneConfig;
    private ISceneLoader _sceneLoader;

    public MainMenuController(MainMenuView mainMenuView, MainMenuSceneConfig mainMenuSceneConfig, 
        ISceneLoader sceneLoader)
    {
        UnityEngine.Debug.Log("MainMenuController");

        _mainMenuView = mainMenuView;
        _mainMenuSceneConfig = mainMenuSceneConfig;
        _sceneLoader = sceneLoader;

        Initialize();
    }

    private void Initialize()
    {
        UnityEngine.Debug.Log("MainMenuController / Initialize");
        _mainMenuView.StartLevelButton.onClick.AddListener(OnClickStartLevelButton);
    }

    private void OnClickStartLevelButton()
    {
        UnityEngine.Debug.Log("MainMenuController / OnClickStartLevelButton");
        _sceneLoader.LoadSceneAsync(_mainMenuSceneConfig.SceneReference).Forget();
    }
}
