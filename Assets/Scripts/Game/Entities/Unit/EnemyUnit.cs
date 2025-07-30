using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyUnit : MonoBehaviour, IUnit
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Animator _animator;
    private NavMeshAgent _agent;

    private UnitConfig _config;

    private IUnitHealth _health;
    private IUnitStateMachine _stateMachine;

    private bool _isSelected;

    [Inject]
    private void Construct(IUnitHealth health)
    {
        _health = health;
    }

    public Transform Transform => this.transform;
    public Rigidbody Rigidbody => _rigidbody;
    public Collider Collider => _collider;
    public Animator Animator => _animator;
    public NavMeshAgent NavMeshAgent => _agent;
    public UnitConfig UnitConfig => _config;
    public IUnitHealth Health => _health;
    public IUnitStateMachine UnitStateMachine => _stateMachine;
    public bool IsSelected { get => _isSelected; set => _isSelected = value; }

    public void SetConfig(UnitConfig config)
    {
        if (config is EnemyUnitConfig enemyUnitConfig)
            _config = enemyUnitConfig;

    }

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
