using UnityEngine;

public interface IContainersCreator
{
    GameObject ContainerRoot { get; }

    Transform CreateContainer(GameObject itemPrefab, string itemName);
}
