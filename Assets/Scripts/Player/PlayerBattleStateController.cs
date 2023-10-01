using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleStateController : UnitBattleStateController
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _resetTime = 2f;
    private Coroutine _resetRoutine;

    private void Awake()
    {
        _input.RMBEvent += OnRMB;
        _input.LMBPressEvent += OnLMBPressed;
        _input.LMBReleaseEvent += OnLMBReleased;
    }
    
    private void OnLMBPressed()
    {
        StopResetting();
        if (_currentState == BattleState.Aim) return;
        SetBattleState(BattleState.Ready);
    }

    private void OnLMBReleased()
    {
        if (_currentState == BattleState.Aim) return;
        SetBattleState(BattleState.Ready);
        StartResetting();
    }

    private void OnRMB(bool aiming)
    {
        var state = aiming ? BattleState.Aim : BattleState.Ready;
        SetBattleState(state);

        if(aiming) StopResetting();
        else StartResetting();
    }

    private void OnDestroy()
    {
        _input.RMBEvent -= OnRMB;
        _input.LMBPressEvent -= OnLMBPressed;
        _input.LMBReleaseEvent -= OnLMBReleased;
    }

    private void StopResetting()
    {
        if (_resetRoutine != null)
        {
            StopCoroutine(_resetRoutine);
            _resetRoutine = null;
        }
    }

    private void StartResetting()
    {
        if (_resetRoutine != null)
        {
            StopCoroutine(_resetRoutine);
            _resetRoutine = null;
        }
        _resetRoutine = StartCoroutine(ResetState());
    }

    private IEnumerator ResetState()
    {
        yield return new WaitForSeconds(_resetTime);
        SetBattleState(BattleState.Regular);
    }
}
