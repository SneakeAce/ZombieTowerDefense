using System.Collections.Generic;

public class UnitHiringButtonsController : IUnitHiringButtonsController
{
    private IPlayerUnitSpawnerManager _playerUnitSpawnerManager;
    private IConfigsProvider _configsProvider;

    private WindowUnitsHiringView _view;

    private ILibraryConfigs<UnitHireButtonConfig> _configs;

    private List<IUnitHiringButton> _hiringButtons = new List<IUnitHiringButton>();

    public UnitHiringButtonsController(IConfigsProvider configsProvider,
        IPlayerUnitSpawnerManager playerUnitSpawnerManager)
    {
        _playerUnitSpawnerManager = playerUnitSpawnerManager;
        _configsProvider = configsProvider;
    }

    public List<IUnitHiringButton> HiringButtons => _hiringButtons;

    public void Initialize()
    {
        GetConfigsLibrary();

        InitializeButton();
    }

    public void GetView(WindowUnitsHiringView view)
    {
        _view = view;
    }

    private void GetConfigsLibrary() => _configs = _configsProvider.GetConfigsLibrary<UnitHireButtonConfig>();

    private void InitializeButton()
    {
        for (int i = 0; i < _view.HireUnitButtons.Count; i++)
        {
            var button = _view.HireUnitButtons[i];
            var config = _configs.Configs[i];

            var hiringButton = new UnitHiringButton(button, config);

            _hiringButtons.Add(hiringButton);
        }
    }
}
