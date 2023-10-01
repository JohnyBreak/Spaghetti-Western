using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : MonoBehaviour
{
    protected UnitBaseState _currentState;
    protected UnitStateFactory _states;
    public UnitBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
}
