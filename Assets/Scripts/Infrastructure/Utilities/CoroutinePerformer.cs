using System.Collections;
using UnityEngine;

public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
{
    public Coroutine StartRoutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void StopRoutine(IEnumerator routine)
    {
        StopCoroutine(routine);
    }

    private void Awake() => DontDestroyOnLoad(this);
}
