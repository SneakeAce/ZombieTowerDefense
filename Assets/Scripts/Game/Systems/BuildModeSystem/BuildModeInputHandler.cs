using UnityEngine;
using UnityEngine.InputSystem;

public class BuildModeInputHandler
{
    private IConfigsProvider _configsProvider;
    private BuildModeInputHandlerConfig _config;

    private LayerMask _cellLayer;

    private Color _colorAvailableCell;
    private Color _colorUnavailableCell;
    private Color _colorSelectedCell;

    private Vector2 _currentMousePosition;
    private IGridCell _previousCell;
    private IGridCell _currentSelectedCell;

    private bool _isWorking = true;

    public BuildModeInputHandler(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;
    }

    public void Initialize()
    {
        GetConfig();

        _cellLayer = _config.CellLayer;

        _colorAvailableCell = _config.ColorAvailableCell;
        _colorUnavailableCell = _config.ColorUnavailableCell;
        _colorSelectedCell = _config.ColorSelectedCell;
    }

    public void CursorMoved(InputAction.CallbackContext context)
    {
        if (_isWorking == false)
            return;

        _currentMousePosition = context.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(_currentMousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _cellLayer)) 
        {
            IGridCell currentCell = hit.collider.GetComponent<IGridCell>();

            if (currentCell != null && currentCell != _previousCell)
            {
                if (_previousCell != null && _currentSelectedCell != null && _currentSelectedCell == _previousCell)
                {
                    _previousCell = null;
                    return;
                }

                _previousCell?.SetDefaultColor(); 
                _previousCell = currentCell;

                if (currentCell.IsEmpty)
                    currentCell.SetColor(_colorAvailableCell);
                else
                    currentCell.SetColor(_colorUnavailableCell);
            }
        }
        else
        {
            if (_previousCell != null && _currentSelectedCell != null && _currentSelectedCell == _previousCell)
            {
                _previousCell = null;
                return;
            }
            else
            {
                _previousCell?.SetDefaultColor();
                _previousCell = null;
            }
        }
    }

    public IGridCell SelectCell()
    {
        if (_previousCell == null || _previousCell.IsEmpty == false || _previousCell.IsSelected)
            return null;

        if (_currentSelectedCell != null)
            return _currentSelectedCell;

        _currentSelectedCell = _previousCell;

        _currentSelectedCell.SelectCell(_colorSelectedCell);
        _currentSelectedCell.OccupyCell();

        _isWorking = false;

        return _currentSelectedCell;
    }

    public void ResetCurrentCell()
    {
        if (_currentSelectedCell == null)
            return;

        _currentSelectedCell.DeselectCell();

        _currentSelectedCell = null;

        _isWorking = true;
    }

    private void GetConfig() => _config = _configsProvider.GetSingleConfig<BuildModeInputHandlerConfig>();
}
