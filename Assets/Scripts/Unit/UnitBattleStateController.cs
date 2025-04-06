using System;
using UnityEngine;

public class UnitBattleStateController : MonoBehaviour
{
    public Action<int> BattleStateChangedEvent;

    [SerializeField] protected int _currentState = BattleState.Regular;
    
    public int CurrentState => _currentState;
    

    public void SetBattleState(int state) 
    {
        _currentState = state;
        BattleStateChangedEvent?.Invoke(_currentState);
    }
}

public class BattleState
{
    public const int Regular = 0;
    public const int Ready = 1;
    public const int Aim = 2;
}