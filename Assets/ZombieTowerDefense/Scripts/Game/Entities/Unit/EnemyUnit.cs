using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyUnit : MonoBehaviour, IUnit
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private UnitConfig _config;

    private IUnitHealth _health;
    private IUnitStateMachine _unitStateMachine;

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
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public UnitConfig UnitConfig => _config;
    public IUnitHealth Health => _health;
    public IUnitStateMachine UnitStateMachine => _unitStateMachine;
    public bool IsSelected { get => _isSelected; set => _isSelected = value; }

    public void SetConfig(UnitConfig config)
    {
        if (config is EnemyUnitConfig enemyUnitConfig)
            _config = enemyUnitConfig;
    }

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _unitStateMachine = GetComponent<UnitStateMachine>();

        _navMeshAgent.speed = _config.UnitMainStats.MoveStats.MoveSpeed;
    }
}
