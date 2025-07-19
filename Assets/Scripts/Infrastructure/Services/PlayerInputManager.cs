public class PlayerInputManager
{
    public PlayerInputManager(PlayerInput playerInput)
    {
        playerInput.Enable();

        playerInput.SelectionUnit.Disable();
        playerInput.BuildMode.Disable();
        playerInput.HireUnits.Disable();
    }
}
