using UnityEngine;

public interface ICoroutinePerformer
{
    void StartRoutine(Coroutine routine);
    void StopRoutine(Coroutine routine);
}
