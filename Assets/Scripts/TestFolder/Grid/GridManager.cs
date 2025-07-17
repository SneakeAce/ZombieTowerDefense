using UnityEngine;

public class GridManager : IGridManager
{
    private const int OffsetFromCenter = 2;
    private const float DefaultCellYPosition = -0.97f;

    private IGridCellFactory _cellfactory;

    private GridManagerConfig _config;
    private GridCell[,] _gridCells; // ƒвойной массив дл€ хранени€ €чеек сетки

    private int _gridWidth;
    private int _gridHeight;
    private float _cellSize;

    private Quaternion _rotationCell;
    private Vector3 _spawnPositionContainer = new Vector3(0f, 0f, 0f);

    private GameObject _cellContainerPrefab;
    private GameObject _cellContainer;

    public GridManager(GridManagerConfig config, IGridCellFactory cellFactory)
    {
        _config = config;
        _cellfactory = cellFactory;

        _gridWidth = _config.GridManagerStats.GridWidth;
        _gridHeight = _config.GridManagerStats.GridHeight;

        _cellSize = _config.GridManagerStats.CellSize;
        _rotationCell = _config.GridManagerStats.RotationCell;

        _cellContainerPrefab = _config.GridManagerStats.CellContainer;

        GeneratedGrid();
    }

    public void ToggleGridActivity(bool isActive)
    {
        _cellContainer.SetActive(isActive);
    }

    private void GeneratedGrid()
    {
        _cellContainer = GameObject.Instantiate(_cellContainerPrefab, _spawnPositionContainer, Quaternion.identity);

        _gridCells = new GridCell[_config.GridManagerStats.GridWidth, _config.GridManagerStats.GridHeight];

        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                float posX = (x - _gridWidth / OffsetFromCenter) * _cellSize;
                float posZ = (y - _gridHeight / OffsetFromCenter) * _cellSize;

                Vector3 positionCell = new Vector3(posX, DefaultCellYPosition, posZ);
                Quaternion rotationCell = _rotationCell;

                GridCellCreatingArguments arguments = new GridCellCreatingArguments(positionCell,
                    rotationCell,
                    _cellContainer.transform, 
                    _config.GridManagerStats.GridCellPrefab);

                GridCell cell = _cellfactory.CreateObject<GridCell, GridCellCreatingArguments>(arguments);

                _gridCells[x, y] = cell;
            }
        }

        ToggleGridActivity(false);
    }

}
