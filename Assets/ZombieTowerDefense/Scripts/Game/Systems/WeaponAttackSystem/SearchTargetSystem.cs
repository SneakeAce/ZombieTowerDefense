using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public struct SearchTargetData
{
    public SearchTargetData(float searchRadius, Vector3 startPosition, LayerMask layerMask)
    {
        SearchRadius = searchRadius;
        StartPosition = startPosition;
        LayerMask = layerMask;
    }

    public float SearchRadius { get; }
    public Vector3 StartPosition { get; }
    public LayerMask LayerMask { get; }
}

public class SearchTargetSystem
{
    private const float MinDelayBeforeCheck = 0.2f;
    private const float MaxDelayBeforeCheck = 0.6f;

    private const int MaxTargetsInBuffer = 16;

    private ICoroutinePerformer _coroutinePerformer;
    private Coroutine _searchTargetCoroutine;
    private Coroutine _checkDistanceToTargetCoroutine;

    private Collider[] _bufferTargets;
    private Vector3 _currentPosition;
    private LayerMask _layerMask;

    private IUnit _nearestTarget;

    private float _searchRadius;
    private float _sqrSearchRadius;

    public SearchTargetSystem(ICoroutinePerformer coroutinePerformer)
    {
        _coroutinePerformer = coroutinePerformer;
    }

    public event Action<IUnit> IsTargetFound;
    public event Action<IUnit> IsTargetDissapeared;

    public void StartSearch(SearchTargetData searchTargetData)
    {
        SetParameters(searchTargetData);

        _bufferTargets = new Collider[MaxTargetsInBuffer];

        if (_searchTargetCoroutine != null)
        {
            _coroutinePerformer.StopRoutine(_searchTargetCoroutine);
            _searchTargetCoroutine = null;
        }

        _searchTargetCoroutine = _coroutinePerformer.StartRoutine(SearchTargetJob());
    }

    private void SetParameters(SearchTargetData searchTargetData)
    {
        _searchRadius = searchTargetData.SearchRadius;
        _currentPosition = searchTargetData.StartPosition;
        _layerMask = searchTargetData.LayerMask;

        _sqrSearchRadius = _searchRadius * _searchRadius;
    }

    private IEnumerator SearchTargetJob()
    {
        while (_nearestTarget == null)
        {
            float randDelay = Random.Range(MinDelayBeforeCheck, MaxDelayBeforeCheck);

            yield return new WaitForSeconds(randDelay);

            int targets = Physics.OverlapSphereNonAlloc(
                _currentPosition, 
                _searchRadius, 
                _bufferTargets, 
                _layerMask);

            for (int i = 0; i < targets; i++) 
            {
                Collider targetCol = _bufferTargets[i];

                bool canMakeTarget = CheckDistanceToTarget(targetCol);

                if (canMakeTarget)
                {
                    if (targetCol.TryGetComponent<IUnit>(out IUnit target))
                        _nearestTarget = target;
                    else
                        Debug.LogError("Component IUnit on target not found!");

                    break;
                }
            }
        }

        TargetFound();
    } 

    private IEnumerator CheckDistanceToTargetJob()
    {
        Collider nearestTargetCol = _nearestTarget.Collider;

        while (_nearestTarget != null)
        {
            yield return new WaitForSeconds(MinDelayBeforeCheck);

            bool targetStillClose = CheckDistanceToTarget(nearestTargetCol);

            if (targetStillClose == false)
                _nearestTarget = null;
        }

        TargetDisapperead();
    }

    private bool CheckDistanceToTarget(Collider target)
    {
        if (target == null)
            return false;

        float sqrDistance = (target.transform.position - _currentPosition).sqrMagnitude;

        if (sqrDistance <= _sqrSearchRadius)
            return true;

        return false;
    }

    private void TargetFound()
    {
        IsTargetFound?.Invoke(_nearestTarget);

        StopCoroutine(ref _searchTargetCoroutine);

        _checkDistanceToTargetCoroutine = RestartCoroutine(ref _checkDistanceToTargetCoroutine, 
            CheckDistanceToTargetJob());
    }

    private void TargetDisapperead()
    {
        _nearestTarget = null;

        IsTargetDissapeared?.Invoke(_nearestTarget);

        StopCoroutine(ref _checkDistanceToTargetCoroutine);

        _searchTargetCoroutine = RestartCoroutine(ref _searchTargetCoroutine, 
            SearchTargetJob());
    }

    private Coroutine RestartCoroutine(ref Coroutine coroutine, IEnumerator routine)
    {
        if (coroutine != null)
        {
            _coroutinePerformer.StopRoutine(coroutine);
            coroutine = null;
        }

        coroutine = _coroutinePerformer.StartRoutine(routine);
        return coroutine;
    }

    private void StopCoroutine(ref Coroutine routine)
    {
        if (routine != null)
        {
            _coroutinePerformer.StopRoutine(routine);
            routine = null;
        }
    }

}
