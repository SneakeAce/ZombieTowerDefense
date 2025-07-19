using System.Collections.Generic;
using Zenject;

public class HiringUnitButtonsController : IHiringUnitButtonsController, IInitialize
{
    private IPlayerUnitSpawnerManager _spawnerManager;

    [Inject] private LazyInject<WindowHiringUnitsView> _lazyView;
    private WindowHiringUnitsView _view;

    private HireUnitButtonConfigsLibrary _configs;

    private List<IHiringUnitButton> _hiringButtons = new List<IHiringUnitButton>();

    public HiringUnitButtonsController(HireUnitButtonConfigsLibrary configs,
        IPlayerUnitSpawnerManager spawnerManager)
    {
        _spawnerManager = spawnerManager;
        _configs = configs;
    }

    public List<IHiringUnitButton> HiringButtons => _hiringButtons;

    public void Initialize()
    {
        _view = _lazyView.Value;

        for (int i = 0; i < _view.HireUnitButtons.Count; i++)
        {
            var button = _view.HireUnitButtons[i];
            var config = _configs.Configs[i];

            var hiringButton = new HiringUnitButton(button, config, _spawnerManager.OnTrySpawn);

            _hiringButtons.Add(hiringButton);
        }
    }
}
