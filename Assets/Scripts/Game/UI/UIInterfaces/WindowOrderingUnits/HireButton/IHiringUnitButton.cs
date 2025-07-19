using System;
using UnityEngine;

public interface IHiringUnitButton
{
    event Action ButtonWasPressed;
    void SetPosition(Vector3 position);
}
