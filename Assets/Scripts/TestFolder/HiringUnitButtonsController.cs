using System.Collections.Generic;
using Zenject;

public class HiringUnitButtonsController : IHiringUnitButtonsController
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

    public void Initialization()
    {
        _view = _lazyView.Value;

        for (int i = 0; i < _view.HireUnitButtons.Count; i++)
        {
            var button = _view.HireUnitButtons[i];
            var config = _configs.Configs[i];

            var hiringButton = new HiringUnitButton();
            hiringButton.SetParameters(button, config, _spawnerManager.OnTrySpawn);

            _hiringButtons.Add(hiringButton);
        }
    }
}
