using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class UnitBattleStateController : MonoBehaviour
{
    public Action<BattleState> BattleStateChangedEvent;

    [SerializeField] protected BattleState _currentState;
    
    public BattleState CurrentState => _currentState;
    
    void Start()
    {
        SetBattleState(BattleState.Regular);
    }

    public void SetBattleState(BattleState state) 
    {
        _currentState = state;
        BattleStateChangedEvent?.Invoke(_currentState);
    }
}

public enum BattleState
{
    Regular,
    Ready,
    Aim,
}