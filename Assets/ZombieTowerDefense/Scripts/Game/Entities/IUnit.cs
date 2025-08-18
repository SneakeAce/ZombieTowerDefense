using UnityEngine;
using UnityEngine.AI;

public interface IUnit : IInitialize
{
    Transform Transform { get; }
    Rigidbody Rigidbody { get; }
    Collider Collider { get; }
    Animator Animator { get; }
    NavMeshAgent NavMeshAgent { get; }
    IUnitHealth Health { get; }
    IUnitStateMachine UnitStateMachine { get; }
    bool IsSelected { get; set; }

    void SetConfig (UnitConfig config);
}
