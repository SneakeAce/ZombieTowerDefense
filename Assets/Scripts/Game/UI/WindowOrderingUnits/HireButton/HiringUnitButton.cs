using System;
using UnityEngine;
using UnityEngine.UI;

public class HiringUnitButton : IHiringUnitButton
{
    private Button _button;
    private HireUnitButtonConfig _config;

    private Vector3 _position;

    public Action<UnitType, Vector3> _onHireRequested;

    public event Action ButtonWasPressed;

    public HiringUnitButton(Button button, HireUnitButtonConfig config, Action<UnitType, Vector3> onHireRequested)
    {
        _button = button;
        _config = config;
        _onHireRequested = onHireRequested;

        _button.onClick.AddListener(OnClick);
    }

    public void SetPosition(Vector3 position) => _position = position;

    private void OnClick()
    {
        _onHireRequested?.Invoke(_config.UnitType, _position);
        ButtonWasPressed?.Invoke();
    }
}
