using System;
using UnityEngine;

[Serializable]
public class GridManagerStats
{
    [field: SerializeField] public int GridWidth { get; private set; }
    [field: SerializeField] public int GridHeight { get; private set; }
    [field: SerializeField] public float CellSize { get; private set; }
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    [field: SerializeField] public LayerMask ObstacleLayer { get; private set; }
    [field: SerializeField] public Quaternion RotationCell { get; private set; }
    [field: SerializeField] public GridCell GridCellPrefab { get; private set; }
    [field: SerializeField] public GameObject CellContainer { get; private set; }
}
