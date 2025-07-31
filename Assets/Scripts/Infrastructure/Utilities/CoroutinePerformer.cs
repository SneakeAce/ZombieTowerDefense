using System.Collections;
using UnityEngine;

public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
{
    public Coroutine StartRoutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void StopRoutine(Coroutine routine)
    {
        StopCoroutine(routine);
    }

    private void Awake() => DontDestroyOnLoad(this);
}
