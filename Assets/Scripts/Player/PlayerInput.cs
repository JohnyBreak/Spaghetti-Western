using UnityEngine;

public class PlayerInput : MonoBehaviour, IService
{
    public event System.Action LMBPressEvent;
    public event System.Action LMBReleaseEvent;
    public event System.Action<bool> LMBEvent;
    public event System.Action<bool> RMBEvent;
    public event System.Action<bool> ShiftEvent;
    public event System.Action<bool> ReloadEvent;
    public event System.Action<int> NumberPressEvent;
    public event System.Action SpacePressEvent;
    public event System.Action<int> MouseScrollEvent;

    private bool _aimPressed;
    private Vector3 _moveVector;
    private bool _isMovementPressed;
    public bool IsMovementPressed => _isMovementPressed;
    public bool IsSprinting { get; private set; }

    public bool AimPressed
    {
        get { return _aimPressed; }
        private set { _aimPressed = value; }
    }

    public Vector3 MoveVector
    {
        get { return _moveVector.normalized; }
        private set { _moveVector = value; }
    }

    public Vector3 NormalizedMoveVector
    {
        get { return _moveVector.normalized; }
    }

    void Update()
    {
        _moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _isMovementPressed = _moveVector != Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LMBPressEvent?.Invoke();
            LMBEvent?.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            LMBReleaseEvent?.Invoke();
            LMBEvent?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _aimPressed = true;
            RMBEvent?.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _aimPressed = false;
            RMBEvent?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsSprinting = true;
            ShiftEvent?.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsSprinting = false;
            ShiftEvent?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadEvent?.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            ReloadEvent?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NumberPressEvent?.Invoke(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            NumberPressEvent?.Invoke(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            NumberPressEvent?.Invoke(3);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpacePressEvent?.Invoke();
        }
        
        if (Input.mouseScrollDelta.y > 0)
        {
            MouseScrollEvent?.Invoke(1);
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            MouseScrollEvent?.Invoke(-1);
        }
    }
}
