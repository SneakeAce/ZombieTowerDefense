using System.Collections;
using UnityEngine;

public struct WeaponAttackData
{
    public WeaponAttackData(IEnemyUnit target, Transform startPosition, LayerMask enemyLayer,
        int damageDealt ,float delayBeforeAttack, float attackRange)
    {
        Target = target;
        StartPosition = startPosition;
        EnemyLayer = enemyLayer;
        DamageDealt = damageDealt;
        DelayBeforeAttack = delayBeforeAttack;
        AttackRange = attackRange;
    }

    public IEnemyUnit Target { get; }
    public Transform StartPosition { get; }
    public LayerMask EnemyLayer { get; }
    public int DamageDealt { get; }
    public float DelayBeforeAttack { get; }
    public float AttackRange { get; }
}

public class WeaponAttackController
{
    private RotateToTarget _rotateToTarget;

    private ICoroutinePerformer _coroutinePerformer;
    private Coroutine _attackCoroutine;

    private IPlayerUnit _playerUnit;

    private IEnemyUnit _target;
    private Transform _spawnRayPosition;
    private LayerMask _enemyLayer;
    private int _damageDealt;
    private float _delayBeforeAttack;
    private float _attackRange;

    public WeaponAttackController(RotateToTarget rotateToTarget, ICoroutinePerformer coroutinePerformer)
    {
        _rotateToTarget = rotateToTarget;
        _coroutinePerformer = coroutinePerformer;
    }

    public void Initialize(IPlayerUnit playerUnit, WeaponAttackData weaponAttackData)
    {
        _playerUnit = playerUnit;

        SetParameters(weaponAttackData);

        _rotateToTarget.Initialize(_playerUnit, _target);
    }

    public void Attack()
    {
        Debug.Log($"WeaponAttackController. Attack");

        if (_attackCoroutine != null)
        {
            _coroutinePerformer.StopRoutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        _attackCoroutine = _coroutinePerformer.StartRoutine(AttackJob());
    }

    public void StopAttack()
    {
        _target = null;

        if (_attackCoroutine != null)
        {
            _coroutinePerformer.StopRoutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        _rotateToTarget.StopRotateToTarget();
    }

    private void SetParameters(WeaponAttackData weaponAttackData)
    {
        _target = weaponAttackData.Target;
        _enemyLayer = weaponAttackData.EnemyLayer;
        _spawnRayPosition = weaponAttackData.StartPosition;
        _damageDealt = weaponAttackData.DamageDealt;
        _delayBeforeAttack = weaponAttackData.DelayBeforeAttack;
        _attackRange = weaponAttackData.AttackRange;
    }

    private IEnumerator AttackJob()
    {
        Debug.Log($"WeaponAttackController. AttackJob");

        while (_target != null)
        {
            if (_rotateToTarget.CanAttack == false)
            {
                _rotateToTarget.StartRotateToTarget();
                yield return null;
                continue;
            }
            else
            {
                SpawnRay();

                yield return new WaitForSeconds(_delayBeforeAttack);
            }
        }
    }

    private void SpawnRay()
    {
        Vector3 direction = _target.Transform.position - _spawnRayPosition.position;

        Ray ray = new Ray(_spawnRayPosition.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, _attackRange, _enemyLayer))
        {
            if (hit.collider.TryGetComponent(out IEnemyUnit enemyUnit))
            {
                DamageData damageData = new DamageData(_damageDealt);

                enemyUnit.Health.TakeDamage(damageData);
                Debug.Log("Enemy is Hit!");
            }
            else
            {
                Debug.LogError("This target is not enemy!");
                return;
            }

        }

    }

}
