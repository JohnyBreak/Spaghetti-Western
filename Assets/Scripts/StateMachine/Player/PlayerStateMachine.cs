using UnityEngine;

public class PlayerStates
{
    public const int Idle = 0;
    public const int Run = 1;
    public const int Grounded = 2;
}

public class PlayerStateMachine : UnitStateMachine
{
    #region Fields

    [SerializeField] protected int _currentRootStateName;
    [SerializeField] protected int _currentSubStateName;

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _movementSpeed = 7.5f;
    [SerializeField] private LayerMask _enemyLayer;

    private HealthSystem _healthSystem;
    private PlayerInput _playerInput;
    private MyCharacterController _controller;
    private PlayerLocomotion _playerAnimation;

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


    public HealthSystem HealthSystem => _healthSystem;

    public Vector3 CameraRelativeMovement => _cameraRelativeMovement;

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
        _currentState = _states.GetState(PlayerStates.Grounded);
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
        _controller.ShouldSnapToGround = IsGrounded && !_isJumping;

        _cameraRelativeMovement = GetCameraRelativeMoveDirection();
        _currentState.UpdateStates();
        _currentRootStateName = _states.RootState;
        _currentSubStateName = _states.SubState;
        //_playerAnimation.SetMovementAnimation(GetMagnitudedMoveVectorForAnimation());
        _controller.Move(_appliedMovement);
    }

    private float GetMagnitudedMoveVectorForAnimation() 
    {
        Vector3 animMoveVectorMagnitude = _currentMovement;
        animMoveVectorMagnitude.y = 0;
        return animMoveVectorMagnitude.magnitude;
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
}
