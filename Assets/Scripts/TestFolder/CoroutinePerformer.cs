using UnityEngine;

public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
{
    public void StartRoutine(Coroutine routine)
    {
        StartRoutine(routine);
    }

    public void StopRoutine(Coroutine routine)
    {
        StopCoroutine(routine);
    }

    private void Awake() => DontDestroyOnLoad(this);
}
