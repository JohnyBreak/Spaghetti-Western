using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerBattleStateController : MonoBehaviour
{
    public event Action<BattleState> BattleStateChangedEvent;

    [SerializeField] private GameObject _crosshairImg;

    [SerializeField] private BattleState _currentState;
    [SerializeField] private float _resetTime = 2f;
    public BattleState CurrentState => _currentState;
    private Coroutine _resetRoutine;
    //[SerializeField] private bool _isShooting = false;

    //private Coroutine _rigWeightRoutine;

    void Start()
    {
        SetBattleState(BattleState.Regular);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //_isShooting = true;
            StopResetting();
            if (_currentState == BattleState.Aim) return;
            SetBattleState(BattleState.Ready);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetBattleState(BattleState.Aim);
            StopResetting();
            return;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            SetBattleState(BattleState.Ready);//BattleState.Regular);
            //if (_isShooting) return;
            StartResetting();
            return;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //_isShooting = false;

            if (_currentState == BattleState.Aim) return;

            SetBattleState(BattleState.Ready);
            StartResetting();
            return;
        }
        //if (Input.GetKeyDown(KeyCode.E)) SetButtleState(BattleState.Regular);
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

    public void SetBattleState(BattleState state) 
    {
        //_regularCamera.SetActive(false);
        //_readyCamera.SetActive(false);
        //_aimCamera.SetActive(false);
        _currentState = state;
        //_crosshairImg.SetActive(true);

        //_playerRegularRotation.enabled = false;
        //_playerReadyRotation.enabled = false;
        //_playerAimRotation.enabled = false;

        //switch (state)
        //{
        //    case BattleState.Regular:
        //        _currentState = BattleState.Regular;
        //        //_regularCamera.SetActive(true);
        //        //_crosshairImg.SetActive(false);
        //        //_playerRegularRotation.enabled = true;

        //        //if (_rigWeightRoutine != null) 
        //        //{
        //        //    StopCoroutine(_rigWeightRoutine);
        //        //    _rigWeightRoutine = null;
        //        //}
        //        //_rigWeightRoutine = StartCoroutine(SmoothRig(0));
        //        break;
        //    case BattleState.Ready:
        //        //_currentState = BattleState.Ready;
        //        //_readyCamera.SetActive(true);
        //        //_playerReadyRotation.enabled = true;
        //        //_playerAimRotation.enabled = true;
        //        //if (_rigWeightRoutine != null)
        //        //{
        //        //    StopCoroutine(_rigWeightRoutine);
        //        //    _rigWeightRoutine = null;
        //        //}
        //        //_rigWeightRoutine = StartCoroutine(SmoothRig(1));
        //        break;
        //    case BattleState.Aim:
        //        _currentState = BattleState.Aim;
        //        //_aimCamera.SetActive(true);
        //        //_playerAimRotation.enabled = true;

        //        //if (_rigWeightRoutine != null)
        //        //{
        //        //    StopCoroutine(_rigWeightRoutine);
        //        //    _rigWeightRoutine = null;
        //        //}
        //        //_rigWeightRoutine = StartCoroutine(SmoothRig(1));
        //        break;
        //}

        BattleStateChangedEvent?.Invoke(_currentState);
    }


}

public enum BattleState
{
    Regular,
    Ready,
    Aim,
}