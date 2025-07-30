public class PlayerInputManager : IInitialize
{
    private PlayerInput _playerInput;

    public PlayerInputManager(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    public void Initialize()
    {
        ActivatePlayerInput();
    }

    private void ActivatePlayerInput()
    {
        _playerInput.Enable();

        _playerInput.SelectionUnit.Disable();
        _playerInput.BuildMode.Disable();
        _playerInput.HireUnits.Disable();
    }

}
