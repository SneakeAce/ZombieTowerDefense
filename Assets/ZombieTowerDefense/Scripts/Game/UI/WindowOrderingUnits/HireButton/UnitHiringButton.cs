using System;
using UnityEngine.UI;

public class UnitHiringButton : IUnitHiringButton
{
    private Button _button;
    private UnitHireButtonConfig _config;

    public event Action<PlayerUnitType> OnHireRequested;
    public event Action ButtonWasPressed;

    public UnitHiringButton(Button button, UnitHireButtonConfig config)
    {
        _button = button;
        _config = config;

        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        OnHireRequested?.Invoke(_config.UnitType);
        ButtonWasPressed?.Invoke();
    }
}
