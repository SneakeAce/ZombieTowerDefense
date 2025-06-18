using UnityEngine;

public interface IObjectPool
{
    public Object GetObjectFromPool();

    public void ReturnPoolObject(Object poolObject);
}
