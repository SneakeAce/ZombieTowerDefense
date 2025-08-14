using System;
using UnityEngine;

public class ContainersCreator :  IContainersCreator
{
    private GameObject _containerRoot;

    public ContainersCreator(GameObject containerRoot)
    {
        _containerRoot = containerRoot;
    }

    public GameObject ContainerRoot => _containerRoot; 

    public Transform CreateContainer(GameObject itemPrefab, string nameContainer)
    {
        if (itemPrefab == null)
            throw new NullReferenceException("[ContainersCreator] - CreateContainer, itemPrefab is null.");

        GameObject newContainer = GameObject.Instantiate(itemPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);

        if (newContainer == null)
            return null;

        newContainer.name = nameContainer;

        return newContainer.transform;
    }
}
