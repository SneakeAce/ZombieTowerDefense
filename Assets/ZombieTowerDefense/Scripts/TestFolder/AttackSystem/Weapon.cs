using System;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour
{
    private WeaponConfig _config;

    private SearchTargetSystem _searchTargetSystem;
    private WeaponAttackController _weaponAttackController;

    private IEnemyUnit _currentTarget;
    private IPlayerUnit _playerUnit;

    [Inject]
    private void Construct(SearchTargetSystem searchTargetSystem,
        WeaponAttackController weaponAttackController)
    {
        Debug.Log("Weapon Construct");
        _searchTargetSystem = searchTargetSystem;
        _weaponAttackController = weaponAttackController;
    }

    public IEnemyUnit Target => _currentTarget;

    public void Initialize(IPlayerUnit playerUnit, WeaponConfig config)
    {
        _playerUnit = playerUnit;
        _config = config;

        StartSearchTarget();
    }

    private void StartSearchTarget()
    {
        SearchTargetData data = new SearchTargetData(
            _config.WeaponStats.RadiusAttack,
            transform.position,
            _config.WeaponStats.EnemyLayer);

        _searchTargetSystem.StartSearch(data);

        _searchTargetSystem.IsTargetFound += SetTarget;
        _searchTargetSystem.IsTargetDissapeared += ResetTarget;
    }

    private void SetTarget(IUnit target)
    {
        Debug.Log($"Weapon. SetTarget(IEnemyUnit target). target = {target}");

        if (target == null)
            return;
        
        if (target is IEnemyUnit enemy)
        {
            _currentTarget = enemy;

            StartAttack();
        }
        else
        {
            Debug.LogWarning($"Weapon. SetTarget. target is not IEnemyUnit. His type = {target}");
        }

        Debug.Log($"Weapon. SetTarget(IEnemyUnit target). _currentTarget = {_currentTarget}");
    }

    private void ResetTarget(IUnit target)
    {
        if (target == null)
        {
            _currentTarget = null;

            StopAttack();
        }
    }

    private void StartAttack()
    {
        Debug.Log($"Weapon. StartAttack");

        WeaponAttackData attackData = new WeaponAttackData(
            _currentTarget,
            _config.WeaponStats.SpawnRayPoint.transform,
            _config.WeaponStats.EnemyLayer,
            _config.WeaponStats.BaseWeaponDamage,
            _config.WeaponStats.BaseDelayBetweenAttack,
            _config.WeaponStats.RadiusAttack
        );

        _weaponAttackController.Initialize(_playerUnit, attackData);

        _weaponAttackController.Attack();
    }

    private void StopAttack()
    {
        _weaponAttackController.StopAttack();
    }

    private void OnDestroy()
    {
        if (_searchTargetSystem == null)
            return;

        _searchTargetSystem.IsTargetFound -= SetTarget;
        _searchTargetSystem.IsTargetDissapeared -= ResetTarget;
    }
}
