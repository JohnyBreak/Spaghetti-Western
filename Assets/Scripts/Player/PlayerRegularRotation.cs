using UnityEngine;

public class PlayerRegularRotation : MonoBehaviour, IInitializable
{
    [SerializeField] private float _rotationSpeed = 15f;
    //[SerializeField] 
    private PlayerInput _playerInput;
    private Transform _camTransform;

    public void Init()
    {
        _playerInput = ServiceLocator.Current.Get<PlayerInput>();
    }

    void Start()
    {
        _camTransform = Camera.main.transform;
    }

    void Update()
    {
        if (_playerInput == null) 
        {
            return;
        }

        if (_playerInput.MoveVector != Vector3.zero)
        {
            Vector3 cameraEuler = Quaternion.Euler(0, _camTransform.eulerAngles.y, 0) * _playerInput.MoveVector;
            Vector3 movementDirection = cameraEuler.normalized;

            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }
}
}
