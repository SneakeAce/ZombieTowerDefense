using System.Collections;
using UnityEngine;

public class RotateToTarget
{
    private const float MinAngleBetweenObjects = 0.1f; 

    private ICoroutinePerformer _coroutinePerformer;
    private Coroutine _rotateToTargetCoroutine;

    private IPlayerUnit _playerUnit;
    private IUnit _currentTarget;

    private float _rotationSpeed;
    private bool _canAttack;

    public RotateToTarget(ICoroutinePerformer coroutinePerformer)
    {
        _coroutinePerformer = coroutinePerformer;
    }

    public bool CanAttack => _canAttack;

    public void Initialize(IPlayerUnit playerUnit, IUnit target)
    {
        SetParameters(playerUnit, target);
    }

    public void StartRotateToTarget()
    {
        if (_rotateToTargetCoroutine != null)
        {
            _coroutinePerformer.StopRoutine(_rotateToTargetCoroutine);
            _rotateToTargetCoroutine = null;
        }

        _rotateToTargetCoroutine = _coroutinePerformer.StartRoutine(RotateToTargetJob());
    }

    public void StopRotateToTarget()
    {
        if (_rotateToTargetCoroutine != null)
        {
            _coroutinePerformer.StopRoutine(_rotateToTargetCoroutine);
            _rotateToTargetCoroutine = null;
        }

        _canAttack = false;
        _currentTarget = null;
    }

    private void SetParameters(IPlayerUnit playerUnit, IUnit target)
    {
        _playerUnit = playerUnit;
        _currentTarget = target;

        _rotationSpeed =  _playerUnit.UnitConfig.UnitMainStats.MoveStats.RotationSpeed;
    }

    private IEnumerator RotateToTargetJob()
    {
        while (_currentTarget != null)
        {
            Vector3 direction = _currentTarget.Transform.position - _playerUnit.Transform.position;
            direction.y = 0f;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            float angle = Quaternion.Angle(_playerUnit.Transform.rotation, lookRotation);

            if (angle > MinAngleBetweenObjects)
            {
                Debug.Log($"RotateToTarget. RotateJob. 'if Angle...'. ");

                _canAttack = false;

                float rotationSpeed = Time.deltaTime * _rotationSpeed;

                Quaternion rotationToTarget = Quaternion.Slerp(_playerUnit.Transform.rotation, lookRotation, rotationSpeed);

                _playerUnit.Transform.rotation = rotationToTarget;
            }
            else
            {
                Debug.Log($"RotateToTarget. RotateJob. else...");
                _canAttack = true;
            }


            yield return null;
        }
    }

}
