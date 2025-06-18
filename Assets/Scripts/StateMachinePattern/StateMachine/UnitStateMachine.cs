using UnityEngine;

public class UnitStateMachine : MonoBehaviour, IUnitStateMachine
{
    private IUnitState _currentState;

    public void SetState(IUnitState state)
    {
        _currentState?.Exit();

        _currentState = state;

        _currentState?.Enter();
    }

    void Update()
    {
        _currentState?.Update();        
    }
}
