using UnityEngine;
using Zenject;

public class Entity : MonoBehaviour, IEntity
{
    private IEntityHealth _health;
    private EntityConfig _config;

    [Inject]
    private void Construct(IEntityHealth health)
    {
        _health = health;
    }

    public void SetConfig(EntityConfig config) => _config = config; 

    public Rigidbody Rigidbody => throw new System.NotImplementedException();
    public Collider Collider => throw new System.NotImplementedException();
    public Animator Animator => throw new System.NotImplementedException();
    public IEntityHealth Health => _health;
}
