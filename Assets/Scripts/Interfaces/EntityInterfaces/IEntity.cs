using UnityEngine;

public interface IEntity
{
    Rigidbody Rigidbody { get; }
    Collider Collider { get; }
    Animator Animator { get; }
    IEntityHealth Health { get; }
}
