using DG.Tweening;
using UnityEngine;

public class MoveState : IUnitState
{
    private IUnit _unit;
    private Vector3 _pointToMove;

    public MoveState(IUnit unit, Vector3 pointToMove)
    {
        _unit = unit;
        _pointToMove = pointToMove;
    }

    public void Enter()
    {
        Vector3 offset = Vector3.up * _unit.Collider.bounds.extents.y;
        Vector3 target = _pointToMove + offset;

        float distance = Vector3.Distance(_unit.Transform.position, target);
        float duration = distance / _unit.UnitConfig.MoveStats.MoveSpeed;

        _unit.Rigidbody.DOMove(target, duration).SetEase(Ease.Linear);
    }

    public void Exit()
    {
        Debug.Log("MoveState Exit");
    }

    public void Update()
    {
        Debug.Log("MoveState Update");
    }
}
