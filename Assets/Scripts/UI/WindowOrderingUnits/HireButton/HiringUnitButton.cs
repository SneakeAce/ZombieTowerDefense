using System;
using UnityEngine.UI;

public class HiringUnitButton : IHiringUnitButton
{
    private Button _button;
    private HireUnitButtonConfig _config;
    public Action<UnitType> _onHireRequested;

    public HiringUnitButton()
    {
    }

    public void SetParameters(Button button, HireUnitButtonConfig config, Action<UnitType> onHireRequested)
    {
        _button = button;
        _config = config;
        _onHireRequested = onHireRequested;

        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _onHireRequested?.Invoke(_config.UnitType);
    }
}
