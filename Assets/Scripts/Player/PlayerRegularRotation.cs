using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegularRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 15f;
    //[SerializeField] 
    private PlayerInput _playerInput;
    private Transform _camTransform;

    void Start()
    {
        _camTransform = Camera.main.transform;
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (_playerInput.MoveVector != Vector3.zero)
        {
            Vector3 cameraEuler = Quaternion.Euler(0, _camTransform.eulerAngles.y, 0) * _playerInput.MoveVector;
            Vector3 movementDirection = cameraEuler.normalized;

            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }
}
}
