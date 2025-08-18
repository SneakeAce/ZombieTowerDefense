using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerUnit : MonoBehaviour, IPlayerUnit
{
    private Weapon _weapon;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Animator _animator;
    private PlayerUnitConfig _config;
    private NavMeshAgent _navMeshAgent;
    private IUnitHealth _health;
    private IUnitStateMachine _unitStateMachine;

    private bool _isSelected;

    [Inject]
    private void Construct(IUnitHealth health)
    {
        _health = health;
    }

    public Transform Transform => transform;
    public Rigidbody Rigidbody => _rigidbody;
    public Collider Collider => _collider;
    public Animator Animator => _animator;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public PlayerUnitConfig UnitConfig => _config;
    public IUnitHealth Health => _health;
    public IUnitStateMachine UnitStateMachine => _unitStateMachine;
    public bool IsSelected { get => _isSelected; set => _isSelected = value; }
    public Weapon Weapon => _weapon;

    public void SetConfig(UnitConfig config)
    {
        if (config is not PlayerUnitConfig playerUnitConfig)
            throw new ArgumentException("Invalid config type for PlayerUnit", nameof(config));

        _config = playerUnitConfig;
    }

    public void Initialize()
    {
        UnitComponentInitialize();

        InitializeWeapon();
    }

    private void UnitComponentInitialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _unitStateMachine = GetComponent<UnitStateMachine>();

        _navMeshAgent.speed = _config.UnitMainStats.MoveStats.MoveSpeed;
    }

    private void InitializeWeapon()
    {
        _weapon = GetComponentInChildren<Weapon>();
         
        if (_weapon == null)
            throw new NullReferenceException("Weapon at Unit is null!");

        _weapon.Initialize(this, _config.UnitWeapon.WeaponConfig);
    }
}
