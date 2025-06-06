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
        _unit.Rigidbody.DOMove(_pointToMove, _unit.UnitConfig.MoveStats.MoveSpeed);
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
