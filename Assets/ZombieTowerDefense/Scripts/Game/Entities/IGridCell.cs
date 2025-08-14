using UnityEngine;

public interface IGridCell : IInitialize
{
    bool IsEmpty { get; }
    bool WalkableCell { get; }
    bool IsSelected { get; }
    GameObject GameObject { get; }
    IUnit CurrentUnit { get; }

    void SetUnit(IUnit unit);
    void SetColor(Color color);
    void SetDefaultColor();
    void OccupyCell();
    void FreeCell();
    void SelectCell(Color color);
    void DeselectCell();
}
