using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationHandler : MonoBehaviour
{
    [SerializeField] private PlayerRegularRotation _playerRegularRotation;
    [SerializeField] private PlayerAimRotation _playerAimRotation;

    [SerializeField] private UnitBattleStateController _battleStateController;

    private void Awake()
    {
        _battleStateController.BattleStateChangedEvent += OnBattleStateChanged;
    }

    private void OnDestroy()
    {
        _battleStateController.BattleStateChangedEvent -= OnBattleStateChanged;
    }

    private void OnBattleStateChanged(BattleState state)
    {        
        _playerRegularRotation.enabled = false;
        _playerAimRotation.enabled = false;

        switch (state)
        {
            case BattleState.Regular:
                _playerRegularRotation.enabled = true;
                break;
            case BattleState.Ready:
                _playerRegularRotation.enabled = true;
                break;
            case BattleState.Aim:
                _playerAimRotation.enabled = true;
                break;
        }
    }
}
