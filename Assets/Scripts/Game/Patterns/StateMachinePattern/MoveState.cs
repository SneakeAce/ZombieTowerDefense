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

    }

    public void Exit()
    {
        Debug.Log("MoveState Exit");
    }

    public void Update()
    {
        //if (_unit.NavMeshAgent.hasPath)
            _unit.NavMeshAgent.SetDestination(_pointToMove);
    }
}
