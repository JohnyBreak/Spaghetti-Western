using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestRSM 
{ 

public class PlayerStateMachine : BaseStateMachine<PlayerStateMachine.PlayerStates>
{
    public enum PlayerStates 
    {
        Idle,
        Move,
        Jump,
        Fall,
    }


    private void Start()
    {
        StartMachine();
    }

    private void Update()
    {
        PlayerStates nextState = CurrentState.GetNextState();

        if (_isTransitioningState) return;

        if (nextState.Equals(CurrentState.StateKey))
            CurrentState.UpdateState();
        else
            TransitionToState(nextState);
    }

    private void TransitionToState(PlayerStates nextState)
    {
        _isTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[nextState];
        CurrentState.EnterState();
        _isTransitioningState = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }

}
}