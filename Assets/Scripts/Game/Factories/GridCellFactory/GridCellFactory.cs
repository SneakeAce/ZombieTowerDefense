using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class GridCellFactory : IGridCellFactory
{
    private int _countCells = 0;

    public T CreateObject<T, TArgs>(TArgs args)
        where T : Object
        where TArgs : IFactoryArguments
    {
        if (args is not GridCellCreatingArguments arguments)
            throw new ArgumentException("Invalid arguments provided for SceneObjectFactory.");

        GameObject cellPrefab = arguments.GridCellPrefab.GameObject;
        GameObject cellObject = GameObject.Instantiate(cellPrefab);

        cellObject.name = cellPrefab.name + "_" + _countCells.ToString();
        _countCells++;

        cellObject.transform.position = arguments.Position;
        cellObject.transform.parent = arguments.Parent;
        cellObject.transform.rotation = arguments.Rotation;

        T cellT = cellObject.GetComponent<T>();

        if (cellT == null)
            throw new NullReferenceException("Cell is null!");

        return cellT;
    }
}
