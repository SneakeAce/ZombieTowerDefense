using System.Collections.Generic;
using Zenject;

public class UnitHiringButtonsController : IUnitHiringButtonsController
{
    private IPlayerUnitSpawnerManager _playerUnitSpawnerManager;

    [Inject] private LazyInject<WindowUnitsHiringView> _lazyView;
    private WindowUnitsHiringView _view;

    private UnitHireButtonConfigsLibrary _configs;

    private List<IUnitHiringButton> _hiringButtons = new List<IUnitHiringButton>();

    public UnitHiringButtonsController(UnitHireButtonConfigsLibrary configs,
        IPlayerUnitSpawnerManager playerUnitSpawnerManager)
    {
        _playerUnitSpawnerManager = playerUnitSpawnerManager;
        _configs = configs;
    }

    public List<IUnitHiringButton> HiringButtons => _hiringButtons;

    public void Initialize()
    {
        _view = _lazyView.Value;

        for (int i = 0; i < _view.HireUnitButtons.Count; i++)
        {
            var button = _view.HireUnitButtons[i];
            var config = _configs.Configs[i];

            var hiringButton = new UnitHiringButton(button, config);

            _hiringButtons.Add(hiringButton);
        }
    }
}
