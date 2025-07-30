using System;
using UnityEngine;

public interface IUnitHiringButton
{
    event Action<UnitType> OnHireRequested;
    event Action ButtonWasPressed;
}
