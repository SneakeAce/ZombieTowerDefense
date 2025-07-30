using System.Collections;
using UnityEngine;

public interface ICoroutinePerformer
{
    Coroutine StartRoutine(IEnumerator routine);
    void StopRoutine(IEnumerator routine);
}
