using UnityEngine;

public interface IUnit
{
    Transform Transform { get; }
    Rigidbody Rigidbody { get; }
    Collider Collider { get; }
    Animator Animator { get; }
    PlayerUnitConfig UnitConfig { get; }
    IUnitHealth Health { get; }
    IUnitStateMachine UnitStateMachine { get; }
    bool IsSelected { get; set; }
}
