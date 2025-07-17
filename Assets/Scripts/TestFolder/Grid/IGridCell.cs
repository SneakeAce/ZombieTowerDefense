using UnityEngine;

public interface IGridCell
{
    bool IsEmpty { get; }
    bool WalkableCell { get; }
    bool IsSelected { get; }
    GameObject GameObject { get; }
    IUnit CurrentUnit { get; }

    void SetColor(Color color);
    void SetDefaultColor();
    void OccupyCell();
    void FreeCell();
    void SelectCell(Color color);
    void DeselectCell();
}
