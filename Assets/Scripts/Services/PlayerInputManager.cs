public class PlayerInputManager
{
    public PlayerInputManager(PlayerInput playerInput)
    {
        playerInput.Enable();

        playerInput.SelectionUnit.Disable();
        playerInput.CallWindowHiringUnits.Disable();
        playerInput.HireUnits.Disable();
    }
}
