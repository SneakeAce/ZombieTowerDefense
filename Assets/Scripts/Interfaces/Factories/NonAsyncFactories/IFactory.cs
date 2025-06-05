using UnityEngine;

public interface IFactory
{
    T CreateObject<T>() where T : Object;
}
