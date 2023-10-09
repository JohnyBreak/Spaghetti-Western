using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class PlayerLocomotion : UnitAnimation
{
    [SerializeField] private float _smoothBlend = 0.1f;
    [SerializeField, Min(0.01f)] private float _aimWeight = 1f;
    public enum Stance { None, Idle, Walk, Run }
    //private Vector2 _input;
    private PlayerInput _input;
    private int _upperBoddyLayerIndex;
    private bool _aiming = false;
    private bool _isSprinting = false;
    
    private Coroutine _aimLayerWeightRoutine;
    private Coroutine _movementSpeedRoutine;
    private int _inputYHash = Animator.StringToHash("InputY");
    private int _locoInputXHash = Animator.StringToHash("LocoInputX");
    private int _locoInputYHash = Animator.StringToHash("LocoInputY");
    private int _aimingHash = Animator.StringToHash("Aiming");
    private int _isSprintingHash = Animator.StringToHash("IsSprinting");

    void Awake()
    {
        _upperBoddyLayerIndex = _animator.GetLayerIndex("UpperBody");
        _input = GetComponent<PlayerInput>();
        _input.RMBEvent += OnAim;
        _input.ShiftEvent += OnSprint;
        //_animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        _input.RMBEvent -= OnAim;
    }

    private void OnAim(bool isAiming) 
    {
        _animator.SetBool(_aimingHash, isAiming); _aiming = isAiming;
        UpdateLayer((isAiming) ? _aimWeight : 0);
    }

    private void OnSprint(bool isSprinting) 
    {
        _animator.SetBool(_isSprintingHash, isSprinting);
        _isSprinting = isSprinting;
    }

    private void UpdateLayer(float end)
    {
        if (_aimLayerWeightRoutine != null)
        {
            StopCoroutine(_aimLayerWeightRoutine);
            _aimLayerWeightRoutine = null;
        }
        _aimLayerWeightRoutine = StartCoroutine(SmoothLayer(end));
    }

    private IEnumerator SmoothLayer(float end)
    {
        float elapsedTime = 0;
        float waitTime = 0.2f;
        while (elapsedTime <= waitTime)
        {
            float start = _animator.GetLayerWeight(_upperBoddyLayerIndex);
            _animator.SetLayerWeight(_upperBoddyLayerIndex, start);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _animator.SetLayerWeight(_upperBoddyLayerIndex, end);
    }

    public void SetMovementAnimation(float currentMovement) 
    {
        _animator.SetFloat(_inputYHash, currentMovement, _smoothBlend, Time.deltaTime);
    }

    public void SetStance(Stance stance)
    {

        switch (stance)
        {
            case Stance.Idle:

                StartMovementCoroutine(0f);
                break;
            case Stance.Walk:

                StartMovementCoroutine(.5f);
                break;
            case Stance.Run:
                StartMovementCoroutine(1f);
                break;
        }
    }

    private void StartMovementCoroutine(float value)
    {
        if (_movementSpeedRoutine != null)
        {
            StopCoroutine(_movementSpeedRoutine);
            _movementSpeedRoutine = null;
        }
        _movementSpeedRoutine = StartCoroutine(SetMovementSpeed(value));
    }

    private IEnumerator SetMovementSpeed(float value)
    {
        while (_animator.GetFloat(_inputYHash) < value - 0.05f || _animator.GetFloat(_inputYHash) > value + 0.05f)
        {
            yield return null;
            _animator.SetFloat(_inputYHash, value, _smoothBlend, Time.deltaTime);
        }
        _animator.SetFloat(_inputYHash, value);
    }

    //void Update()
    //{
    //    if (_input.NormalizedMoveVector == Vector3.zero)
    //    {
    //        //_animator.SetFloat("InputX", 0, _smoothBlend, Time.deltaTime);
    //        _animator.SetFloat(_inputYHash, 0, _smoothBlend, Time.deltaTime);
    //        _animator.SetFloat(_locoInputXHash, 0, _smoothBlend, Time.deltaTime);
    //        _animator.SetFloat(_locoInputYHash, 0, _smoothBlend, Time.deltaTime);
    //    }
    //    else
    //    {
    //        //var d = CalcVector();
    //        if (_aiming)//(d != 0)
    //        {
    //            _animator.SetFloat(_locoInputXHash, _input.NormalizedMoveVector.x/* * d*/, _smoothBlend, Time.deltaTime);
    //            _animator.SetFloat(_locoInputYHash, _input.NormalizedMoveVector.z/* * d*/, _smoothBlend, Time.deltaTime);
    //        }
    //        else
    //        {
    //            //_animator.SetFloat("InputX", 0, _smoothBlend, Time.deltaTime);
    //            _animator.SetFloat(_inputYHash, (_isSprinting) ? 1 : 0.5f, _smoothBlend, Time.deltaTime);
    //        }
    //    }
    //}
}
