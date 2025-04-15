using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLayersNames
{
    public const string BaseLayer = "BaseLayer";
    public const string Legs = "Legs";
    public const string UpperBody = "UpperBody";
    public const string RightHandAim = "RightHandAim";
    public const string LeftHandAim = "LeftHandAim";
    public const string Hands = "Hands";
    public const string UpperBodyDamage = "UpperBodyDamage";

    private static string[] _names = new string[] 
    {   
        BaseLayer,
        Legs,
        UpperBody,
        RightHandAim,
        LeftHandAim,
        Hands,
        UpperBodyDamage
    };

    public static int GetIndex(string name) 
    {
        var result = Array.FindIndex(_names, x => x.Contains(name));

        return result;
    }
}

public class PlayerAnimation : UnitAnimation, IInitializable
{
    [SerializeField] private float _smoothBlend = 0.1f;
    //[SerializeField, Min(0.01f)] private float _aimWeight = 1f;
    [SerializeField] private UnitBattleStateController _battleStateController;

    public enum Stance { None, Idle, Walk, Run }
    //private Vector2 _input;
    private PlayerInput _input;
    //private int _upperBoddyLayerIndex;
    private bool _isLocomotion = false;
    private bool _isSprinting = false;
    
    //private Coroutine _aimLayerWeightRoutine;
    private Coroutine _movementSpeedRoutine;
    private int _inputYHash = Animator.StringToHash("InputY");
    private int _locoInputXHash = Animator.StringToHash("LocoInputX");
    private int _locoInputYHash = Animator.StringToHash("LocoInputY");
    private int _locomotionHash = Animator.StringToHash("Locomotion");
    private int _isSprintingHash = Animator.StringToHash("IsSprinting");
    private int _forwardJumpHash = Animator.StringToHash("JumpForward");
    public int ForwardJumpHash => _forwardJumpHash;

    private Dictionary<int, Coroutine> _layersCoroutinesMap = new();

    public void Init()
    {
            _input = ServiceLocator.Current.Get<PlayerInput>();
        //_input.RMBEvent += OnAim;
        _input.ShiftEvent += OnSprint;
    }

    void Awake()
    {
        //_upperBoddyLayerIndex = _animator.GetLayerIndex("UpperBody");
        //_battleStateController.BattleStateChangedEvent += OnAim;
        
        //_animator = GetComponent<Animator>();
    }

    public void CrossFade(int stateNameHash, int layer = 0) 
    {
        _animator.CrossFade(stateNameHash, 0.1f, layer);
    }

    private void OnDestroy()
    {
        //_input.RMBEvent -= OnAim;
        //_battleStateController.BattleStateChangedEvent -= OnAim;
    }

    //private void OnAim(int state) 
    //{
    //    var isLocomotion = (state != BattleState.Regular);
    //    _animator.SetBool(_locomotionHash, isLocomotion); 
    //    _isLocomotion = isLocomotion;
    //    var value = (isLocomotion) ? _aimWeight : 0;
    //    UpdateLayer(AnimationLayersNames.GetIndex(AnimationLayersNames.UpperBody), value);
    //    //UpdateLayer(AnimationLayersNames.GetIndex(AnimationLayersNames.RightHandAim), value);
    //    //UpdateLayer(AnimationLayersNames.GetIndex(AnimationLayersNames.LeftHandAim), value);
    //}

    private void OnSprint(bool isSprinting) 
    {
        if (isSprinting)
        {
            _animator.SetBool(_locomotionHash, false);
            _isLocomotion = false;
        }

        _animator.SetBool(_isSprintingHash, isSprinting);

        _isSprinting = isSprinting;
    }

    private void UpdateLayer(int layer, float end)
    {
        if (_layersCoroutinesMap.ContainsKey(layer))
        {
            StopCoroutine(_layersCoroutinesMap[layer]);
            _layersCoroutinesMap.Remove(layer);
        }
        _layersCoroutinesMap[layer] = StartCoroutine(SmoothLayer(layer, end));
    }

    private IEnumerator SmoothLayer(int layer, float end)
    {
        float elapsedTime = 0;
        //float waitTime = 0.2f;
        while (elapsedTime <= _smoothBlend)
        {
            float start = _animator.GetLayerWeight(layer);
            var weight = Mathf.Lerp(start, end, elapsedTime);

            _animator.SetLayerWeight(layer, weight);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _animator.SetLayerWeight(layer, end);
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

    void Update()
    {
        if (_input.NormalizedMoveVector == Vector3.zero)
        {
            //_animator.SetFloat("InputX", 0, _smoothBlend, Time.deltaTime);
            _animator.SetFloat(_inputYHash, 0, _smoothBlend, Time.deltaTime);
            _animator.SetFloat(_locoInputXHash, 0, _smoothBlend, Time.deltaTime);
            _animator.SetFloat(_locoInputYHash, 0, _smoothBlend, Time.deltaTime);
        }
        else
        {
            //var d = CalcVector();
            if (_isLocomotion)//(d != 0)
            {
                _animator.SetFloat(_locoInputXHash, _input.NormalizedMoveVector.x/* * d*/, _smoothBlend, Time.deltaTime);
                _animator.SetFloat(_locoInputYHash, _input.NormalizedMoveVector.z/* * d*/, _smoothBlend, Time.deltaTime);
            }
            else
            {
                //_animator.SetFloat("InputX", 0, _smoothBlend, Time.deltaTime);
                _animator.SetFloat(_inputYHash, (_isSprinting) ? 1 : 0.5f, _smoothBlend, Time.deltaTime);
            }
        }
    }

    internal void SetLayerWeight(int layer, int weight)
    {
        UpdateLayer(layer, weight);
    }
}
