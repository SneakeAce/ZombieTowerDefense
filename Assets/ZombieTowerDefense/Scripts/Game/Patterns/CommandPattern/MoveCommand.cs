using UnityEngine;

public class MoveCommand : ICommand
{
    private IUnit _unit;
    private Vector3 _pointToMove;

    public MoveCommand(IUnit unit, Vector3 pointToMove)
    {
        _unit = unit;
        _pointToMove = pointToMove;
    }

    public void Execute()
    {
        _unit.UnitStateMachine.SetState(new MoveState(_unit, _pointToMove));
    }
}
