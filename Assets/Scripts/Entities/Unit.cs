using UnityEngine;
using Zenject;

public class Unit : MonoBehaviour, IUnit
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Animator _animator;
    private UnitConfig _config;

    private IUnitHealth _health;
    private IUnitStateMachine _unitStateMachine;

    private bool _isSelected;

    [Inject]
    private void Construct(IUnitHealth health, UnitConfig config)
    {
        _health = health;
        _config = config;
    }

    public Transform Transform => transform;
    public Rigidbody Rigidbody => _rigidbody;
    public Collider Collider => _collider;
    public Animator Animator => _animator;
    public UnitConfig UnitConfig => _config;
    public IUnitHealth Health => _health;
    public IUnitStateMachine UnitStateMachine => _unitStateMachine;
    public bool IsSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;
        }
    }

    //public void SetConfig(UnitConfig config) => _config = config; 

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _unitStateMachine = GetComponent<UnitStateMachine>();
    }



}
