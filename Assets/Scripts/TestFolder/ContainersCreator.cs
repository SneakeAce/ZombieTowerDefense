using System;
using UnityEngine;
using Zenject;

public class ContainersCreator : MonoBehaviour, IContainersCreator
{
    private GameObject _containerRoot;

    [Inject]
    private void Construct(GameObject containerRoot)
    {
        _containerRoot = containerRoot;
    }

    public GameObject ContainerRoot => _containerRoot; 

    public Transform CreateContainer(GameObject itemPrefab, string nameContainer)
    {
        if (itemPrefab == null)
            throw new NullReferenceException("[ContainersCreator] - CreateContainer, itemPrefab is null.");

        GameObject newContainer = Instantiate(itemPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);

        if (newContainer == null)
            return null;

        newContainer.name = nameContainer;

        return newContainer.transform;
    }
}
