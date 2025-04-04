using System;
using UnityEngine;

public class UnitBattleStateController : MonoBehaviour
{
    public Action<string> BattleStateChangedEvent;

    [SerializeField] protected string _currentState = BattleState.Regular;
    
    public string CurrentState => _currentState;
    

    public void SetBattleState(string state) 
    {
        _currentState = state;
        BattleStateChangedEvent?.Invoke(_currentState);
    }
}

public class BattleState
{
    public const string Regular = "Regular";
    public const string Ready = "Ready";
    public const string Aim = "Aim";
}