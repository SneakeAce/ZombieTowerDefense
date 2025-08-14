using System;

public interface IUnitHiringButton
{
    event Action<PlayerUnitType> OnHireRequested;
    event Action ButtonWasPressed;
}
