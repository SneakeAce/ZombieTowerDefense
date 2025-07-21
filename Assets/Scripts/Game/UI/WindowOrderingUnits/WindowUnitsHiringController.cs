public class WindowUnitsHiringController : IWindowUnitsHiringController, IInitialize
{
    private ICameraManager _cameraManager;
    private IWindowUnitsHiringManager _windowHiringUnitsManager;

    private bool _windowIsOpen;

    public WindowUnitsHiringController(ICameraManager cameraManager, 
        IWindowUnitsHiringManager windowHiringUnitsManager)
    {
        _cameraManager = cameraManager;
        _windowHiringUnitsManager = windowHiringUnitsManager;
    }

    public void Initialize() => _windowHiringUnitsManager.WindowOrderingCanvas.gameObject.SetActive(false);
    

    public void ToggleWindowUnitsHiring(bool canBeOpened) 
    {
        _windowHiringUnitsManager.WindowOrderingCanvas.gameObject.SetActive(canBeOpened);
    }

}
