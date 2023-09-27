
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _runSpeed = 5f;
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private Transform _cameraTransform;

    private MyCharacterController _ctrl;
    private float _finalSpeed;
    private void Awake()
    {
        _ctrl = GetComponent<MyCharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.ShiftEvent += OnShift;
        _finalSpeed = _walkSpeed;
    }

    void Update()
    {
        Move();
    }

    private void OnShift(bool pressed) 
    {
        if (pressed) _finalSpeed = _runSpeed;
        else _finalSpeed = _walkSpeed;
    }

    private void Move()
    {
        if (_playerInput.MoveVector != Vector3.zero) 
        {
            Vector3 cameraEuler = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0) * _playerInput.MoveVector;
            Vector3 movementDirection = cameraEuler.normalized;

            _ctrl.SimpleMove(movementDirection * _finalSpeed);
        }
    }

    private void OnDestroy()
    {
        _playerInput.ShiftEvent -= OnShift;
    }
}
