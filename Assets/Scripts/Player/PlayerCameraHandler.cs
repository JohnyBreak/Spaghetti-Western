using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour
{
    [SerializeField] private GameObject _regularCamera;
    [SerializeField] private GameObject _readyCamera;
    [SerializeField] private GameObject _aimCamera;
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
        _regularCamera.SetActive(false);
        _readyCamera.SetActive(false);
        _aimCamera.SetActive(false);

        switch (state)
        {
            case BattleState.Regular:
                _regularCamera.SetActive(true);
                break;
            case BattleState.Ready:
                _readyCamera.SetActive(true);
                break;
            case BattleState.Aim:
                _aimCamera.SetActive(true);
                break;
        }
    }
}
