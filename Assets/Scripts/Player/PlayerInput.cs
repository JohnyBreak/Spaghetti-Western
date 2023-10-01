
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event System.Action LMBPressEvent;
    public event System.Action LMBReleaseEvent;
    public event System.Action<bool> LMBEvent;
    public event System.Action<bool> RMBEvent;
    public event System.Action<bool> ShiftEvent;
    public event System.Action<bool> ReloadEvent;

    private Vector3 _moveVector;

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
            RMBEvent?.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            RMBEvent?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ShiftEvent?.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
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
    }
}
