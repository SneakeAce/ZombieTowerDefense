using UnityEngine;

public interface IObjectPool<T>
    where T : MonoBehaviour
{
    public T GetPoolObject();

    public void ReturnPoolObject(T poolObject);
}
