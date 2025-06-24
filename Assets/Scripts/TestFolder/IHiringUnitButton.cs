using System;
using UnityEngine.UI;

public interface IHiringUnitButton
{
    void SetParameters(Button button, HireUnitButtonConfig config, Action<UnitType> onHireRequested);
}
