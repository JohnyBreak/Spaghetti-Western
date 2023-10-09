using System;
using UnityEngine;

public class UnitStateMachine<EState> : MonoBehaviour where EState : Enum
{
    protected UnitBaseState<EState> _currentState;
    protected UnitStateFactory<EState> _states;
    public UnitBaseState<EState> CurrentState { get { return _currentState; } set { _currentState = value; } }
}
