using UnityEngine;

public class GridCell : MonoBehaviour, IGridCell
{
    private Color _defaultColor;
    private Material _cellMaterial;

    public bool IsEmpty { get; private set; }
    public bool WalkableCell { get; private set; }
    public bool IsSelected { get; private set; }

    public GameObject GameObject => this.gameObject;
    public IPlayerUnit CurrentUnit { get; private set; }

    public void Initialize()
    {
        IsEmpty = true;
        WalkableCell = true;
        IsSelected = false;
        
        _cellMaterial = GetComponent<MeshRenderer>().material;

        Debug.Log($"CellMaterial in GridCell = {_cellMaterial}");
        _defaultColor = _cellMaterial.color;
    }

    public void SetUnit(IPlayerUnit unit) => CurrentUnit = unit;

    public void SetColor(Color color) => _cellMaterial.color = color;
    
    public void SetDefaultColor() => _cellMaterial.color = _defaultColor;

    public void OccupyCell()
    {
        IsEmpty = false;
        WalkableCell = false;
    }

    public void FreeCell()
    {
        IsEmpty = true;
        WalkableCell = true;
    }

    public void SelectCell(Color color)
    {
        IsSelected = true;

        _cellMaterial.color = color;
    }

    public void DeselectCell()
    {
        IsSelected = false;

        _cellMaterial.color = _defaultColor;
    }

}
