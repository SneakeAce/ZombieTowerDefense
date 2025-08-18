using System.Collections;
using UnityEngine;

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

public class SearchTarget
{
    private const float DelayBeforeSearchingTarget = 0.2f;
    private const int MaxTargetsInBuffer = 16;

    private ICoroutinePerformer _coroutinePerformer;
    private Coroutine _startSearchCoroutine;

    private Collider[] _bufferTargets;
    private Vector3 _currentPosition;
    private LayerMask _layerMask;

    private float _searchRadius;
    private bool _isEnable = false;

    public SearchTarget(ICoroutinePerformer coroutinePerformer)
    {
        _coroutinePerformer = coroutinePerformer;
    }

    public void Initialize(SearchTargetData searchTargetData)
    {
        SetParameters(searchTargetData);

        _bufferTargets = new Collider[MaxTargetsInBuffer];
        _isEnable = true;

        _startSearchCoroutine = _coroutinePerformer.StartRoutine(StartSearchJob());
    }

    private void SetParameters(SearchTargetData searchTargetData)
    {
        _searchRadius = searchTargetData.SearchRadius;
        _currentPosition = searchTargetData.StartPosition;
        _layerMask = searchTargetData.LayerMask;
    }

    private IEnumerator StartSearchJob()
    {
        while (_isEnable)
        {
            yield return new WaitForSeconds(DelayBeforeSearchingTarget);

            int targets = Physics.OverlapSphereNonAlloc(
                _currentPosition, 
                _searchRadius, 
                _bufferTargets, 
                _layerMask);



        }
    }
}
