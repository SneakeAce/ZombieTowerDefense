public class WindowUnitsHiringController : IWindowUnitsHiringController, IInitialize
{
    private IWindowUnitsHiringManager _windowHiringUnitsManager;

    public WindowUnitsHiringController(IWindowUnitsHiringManager windowHiringUnitsManager)
    {
        _windowHiringUnitsManager = windowHiringUnitsManager;
    }

    public void Initialize() => _windowHiringUnitsManager.WindowOrderingCanvas.gameObject.SetActive(false);
    
    public void ToggleWindowUnitsHiring(bool canBeOpened) 
    {
        _windowHiringUnitsManager.WindowOrderingCanvas.gameObject.SetActive(canBeOpened);
    }

}
