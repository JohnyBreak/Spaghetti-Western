using UnityEngine;

public class PlayerAimRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 15f;
    private Transform _camTransform;

    void Start()
    {
        _camTransform = Camera.main.transform;
    }

    void Update()
    {
        float yawCamera = _camTransform.rotation.eulerAngles.y;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _rotationSpeed * Time.deltaTime);
    }
}
