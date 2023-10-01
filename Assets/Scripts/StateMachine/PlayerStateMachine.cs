using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateMachine : UnitStateMachine
{
    #region Fields

    [SerializeField] protected PlayerStateFactory.States _currentRootStateName;
    [SerializeField] protected PlayerStateFactory.States _currentSubStateName;

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CinemachineVirtualCamera _aimCamera;
    [SerializeField] private float _movementSpeed = 7.5f;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField, Range(0, 360)] private float _attackCheckAngle;

    [SerializeField] private float _attackCheckRadius;

    private HealthSystem _healthSystem;
    private PlayerInput _playerInput;
    private MyCharacterController _controller;
    private  PlayerLocomotion _playerAnimation;
    //private float _rotationFactorPerFrame = 15f;
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _appliedMovement;

    private Vector3 _cameraRelativeMovement;

    private float _turnSmoothTime = 0.03f;

    private bool _isMovementPressed;

    private float _gravity = -9.8f;
    private float _groundedGravity = -0.05f;
    private bool _isFalling;


    //jump
    private bool _isJumpPressed = false;
    private float _initialJumpVelocity;
    private float _maxJumpHeight = 2f;
    private float _maxJumpTime = 0.75f;
    private bool _isJumping = false;
    private int _maxJumpCount = 1; // 1 less than you need (here i need 2 jump)
    private bool _canRepeatJump = true;
    private int _currentJumpCount;
    private float _coyoteTimer = 0;
    private float _coyoteTime = 0.25f;

    //glide
    private bool _isJumpHold;

    //dash
    private bool _isDashPressed = false;
    private bool _canRepeatDash = true;
    private bool _canRepeatDash2 = true;
    private bool _dashReloaded = true;
    private float _dashTime = .33f;
    private float _dashReloadTime = .44f;
    private float _dashStartTime = 0f;
    private float _dashSpeed = 20f;
    private bool _isDashing;
    private IEnumerator _dashRoutine;

    //aim
    private bool _isAiming = false;

    // attack
    private bool _isAttacking = false;
    private bool _isAttackPressed = false;
    private bool _isAttackHold = false;
    private bool _canRepeatAttack = true;

    //private InputManager _inputManager;
    #endregion

    #region Properties
    // getters & setters
    public PlayerLocomotion PlayerAnimation => _playerAnimation;
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public int MaxJumpCount { get { return _maxJumpCount; } }
    public int CurrentJumpCount { get { return _currentJumpCount; } set { _currentJumpCount = value; } }
    public bool IsJumping { set { _isJumping = value; } }
    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public Vector3 CurrentMovement { get { return _currentMovement; } set { _currentMovement = value; } }
    public float InitialJumpVelocity => _initialJumpVelocity;
    public bool IsGrounded { get { return _controller.IsGrounded; } }
    public bool GroundDiff { get { return _controller.GroundDiff; } }
    public bool CanRepeatJump { get { return _canRepeatJump; } set { _canRepeatJump = value; } }
    public bool IsFalling { get { return _isFalling; } set { _isFalling = value; } }
    public float GroundedGravity => _groundedGravity;
    public float Gravity => _gravity;
    public float MagnitudedMovement => GetMagnitudedMoveVectorForAnimation();
    public float MovementSpeed => _movementSpeed;
    public Transform CameraTransform => _cameraTransform;
    public float TurnSmoothTime => _turnSmoothTime;
    public bool IsMovementPressed => _isMovementPressed;
    public float CoyoteTimer { get { return _coyoteTimer; } set { _coyoteTimer = value; } }
    public float CoyoteTime => _coyoteTime;
    public bool IsJumpHold => _isJumpHold;
    public float DashTime => _dashTime;
    public float DashStartTime { get { return _dashStartTime; } set { _dashStartTime = value; } }
    public float DashSpeed => _dashSpeed;
    public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
    public IEnumerator DashRoutine { get { return _dashRoutine; } set { _dashRoutine = value; } }
    public bool CanRepeatDash { get { return _canRepeatDash; } set { _canRepeatDash = value; } }
    public bool CanRepeatDash2 { get { return _canRepeatDash2; } set { _canRepeatDash2 = value; } }
    public bool DashReloaded { get { return _dashReloaded; } set { _dashReloaded = value; } }

    //public UnitSettings Settings => _settings;
    
    public bool IsDashPressed { get { return _isDashPressed; } set { _isDashPressed = value; } }
    public bool HeadHit => Physics.Raycast(transform.position, Vector3.up, 1.4f, _controller.DiscludePlayerMask);

    public bool IsAiming { get { return _isAiming; } set { _isAiming = value; } }

    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    public bool IsAttackPressed { get { return _isAttackPressed; } set { _isAttackPressed = value; } }
    public bool CanRepeatAttack { get { return _canRepeatAttack; } set { _canRepeatAttack = value; } }
    public bool IsAttackHold { get { return _isAttackHold; } set { _isAttackHold = value; } }
    //public float[] GroundedAttacksLength => _playerAnimation.GroundedAttacksLength;
    public IEnumerator WaitAttackTimeRoutine;

    public HealthSystem HealthSystem => _healthSystem;

    public LayerMask EnemyLayer => _enemyLayer;
    public float AttackCheckAngle => _attackCheckAngle;
    public Vector3 CameraRelativeMovement => _cameraRelativeMovement;
    public float AttackCheckRadius => _attackCheckRadius;

    #endregion

    private void OnEnable()
    {
        //_playerInput.Player.Enable();
    }


    private void Awake()
    {
        Debug.LogError("PlayerStateMachine Awake");
        _controller = GetComponent<MyCharacterController>();
        GameStateManager.GameStateChangedEvent += OnGameStateChange;

        _healthSystem = GetComponentInChildren<HealthSystem>();

         //new PlayerInputActions();
        _states = new PlayerStateFactory(this);
        _currentState = ((PlayerStateFactory)_states).Grounded();
        _currentState.EnterState();


        _healthSystem.DamageEvent += OnDamage;
        _healthSystem.DeathEvent += OnDeath;

       

        SetupJumpVariables();
    }

    protected void OnGameStateChange(GameStateManager.GameState newState)
    {
    }

    protected void OnDamage() 
    {
        //if (_currentState is PlayerDamageGroundState) return;
        //Debug.LogError("OnDamage");
        //_currentState.ExitStates();
        //_currentState = _states.DamageGround();
        //_currentState.EnterState();
    }

    protected void OnDeath()
    {
        //if (_currentState is PlayerDeathState) return;
        //_currentState.ExitStates();
        //_currentState = _states.Death();
        //_currentState.EnterState();
    }

    private void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
    }



    void Update()
    {
        if (GameStateManager.CurrentGameState != GameStateManager.GameState.GamePlay) return;
        //Debug.DrawRay(transform.position, Vector3.up * 1.4f, Color.cyan);
        _controller.CanSnapToGround = IsGrounded && !_isJumping;

        _cameraRelativeMovement = GetCameraRelativeMoveDirection();
        _currentState.UpdateStates();
        _currentRootStateName = (PlayerStateFactory.States)_states.RootState;
        _currentSubStateName = (PlayerStateFactory.States)_states.SubState;
        //_playerAnimation.SetMovementAnimation(GetMagnitudedMoveVectorForAnimation());
        _controller.SimpleMove(_appliedMovement);
    }

    private float GetMagnitudedMoveVectorForAnimation() 
    {
        Vector3 animMoveVectorMagnitude = _currentMovement;
        animMoveVectorMagnitude.y = 0;
        return animMoveVectorMagnitude.magnitude;
    }

    public IEnumerator DashReloadTimer()
    {
        //yield return null;
        yield return new WaitForSeconds(_dashReloadTime);
        _dashReloaded = true;
    }

    private void OnDisable()
    {

    }

    private void OnDestroy() 
    {
        _healthSystem.DamageEvent -= OnDamage;
        _healthSystem.DeathEvent -= OnDeath;
        GameStateManager.GameStateChangedEvent -= OnGameStateChange;
    }

    private Vector3 GetCameraRelativeMoveDirection() 
    {
        Vector3 cameraForward = _cameraTransform.forward;
        Vector3 cameraRight = _cameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 forwardCameraRelativeMovement = cameraForward * _currentMovement.z;
        Vector3 rightCameraRelativeMovement = cameraRight * _currentMovement.x;
        Vector3 cameraRelativeMovement = forwardCameraRelativeMovement + rightCameraRelativeMovement;

        Debug.DrawRay(transform.position, cameraRelativeMovement, Color.black);
        return cameraRelativeMovement;
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, _appliedMovement.normalized, Color.black);
        Handles.color = Color.white;
        Handles.DrawWireArc(transform.position + transform.up * 0.7f, Vector3.up, Vector3.forward, 360, _attackCheckRadius);
        Vector3 viewAngleA = DirectionFromAngle(-_attackCheckAngle / 2, false);
        Vector3 viewAngleB = DirectionFromAngle(_attackCheckAngle / 2, false);

        Handles.DrawLine((transform.position + transform.up * 0.7f), (transform.position + transform.up * 0.7f) + viewAngleA * _attackCheckRadius);
        Handles.DrawLine((transform.position + transform.up * 0.7f), (transform.position + transform.up * 0.7f) + viewAngleB * _attackCheckRadius);
    }
    public Vector3 DirectionFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        angleDegrees += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }
#endif



}
