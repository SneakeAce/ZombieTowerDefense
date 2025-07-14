using UnityEngine;

public class GridCell : MonoBehaviour, IGridCell
{
    public bool IsEmpty => throw new System.NotImplementedException();

    public bool WalkableCell => throw new System.NotImplementedException();

    public GameObject GameObject => this.gameObject;
}
