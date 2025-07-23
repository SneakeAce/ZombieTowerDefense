using System;
using UnityEditor.PackageManager;
using UnityEngine;
using Object = UnityEngine.Object;

public class GridCellFactory : IGridCellFactory
{
    private const float RayOffset = 0.5f;
    private const float RayDistance = 10f;
    private const float SphereRadius = 2f;

    private const int BufferObstaclesSize = 6; 

    private int _countCells = 0;

    private Collider[] _bufferObstacles = new Collider[BufferObstaclesSize];

    public T CreateObject<T, TArgs>(TArgs args)
        where T : Object
        where TArgs : IFactoryArguments
    {
        if (args is not GridCellCreatingArguments arguments)
            throw new ArgumentException("Invalid arguments provided for SceneObjectFactory.");

        GameObject cellPrefab = arguments.GridCellPrefab.GameObject;

        if (CheckGround(arguments.Position, arguments.GroundLayer) == false)
        {
            Debug.Log("Not found ground under cell!");
            return null;
        }

        if (CheckObstacle(arguments.Position, arguments.ObstacleLayer))
        {
            Debug.Log("Found obstacle under cell!");
            return null;
        }

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

    private bool CheckGround(Vector3 position, LayerMask groundLayer)
    {
        Ray ray = new Ray(position + Vector3.up * RayOffset, Vector3.down);
            
        return Physics.Raycast(ray, RayDistance, groundLayer);
    }

    private bool CheckObstacle(Vector3 position, LayerMask obstacleLayer)
    {
        int obstacles = Physics.OverlapSphereNonAlloc(position, 
            SphereRadius, 
            _bufferObstacles, 
            obstacleLayer);
        
        if (obstacles == 0)
            return false;

        for (int i = 0; i < obstacles; i++)
        {
            if (((1 << _bufferObstacles[i].gameObject.layer) & obstacleLayer) != 0)
                return true;
        }

        return false;
    }

}
