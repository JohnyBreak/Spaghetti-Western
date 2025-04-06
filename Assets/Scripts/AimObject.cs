using UnityEngine;

public class AimObject : MonoBehaviour
{
    [SerializeField] private Transform _aimObject;
    [SerializeField] private Transform _shootObject;
    [SerializeField] private float _distance = 50f;
    [SerializeField, Min(0.01f)] private float _gizmoRadius = 0.1f;
    [SerializeField] private float _speed = 50f;
    [SerializeField] private LayerMask _mask;
    private Vector3 _screenPoint = new Vector3(0.5f, 0.5f, 0.5f);
    private Transform _camTransform;

    void Start()
    {
        _camTransform = Camera.main.transform;
    }

    void Update()
    {
        Debug.DrawRay(_camTransform.position, _camTransform.forward * _distance, Color.green);

        RaycastHit hit;

        Ray ray = new Ray(_camTransform.position, _camTransform.forward * _distance);
        Vector3 tempPos; 
        if (Physics.Raycast(ray, out hit, _distance, _mask))
        {
            tempPos = hit.point;
            //_aimObject.transform.position = hit.point;
        }
        else
        {
            tempPos = _camTransform.transform.position + _camTransform.forward * _distance;
            //_aimObject.transform.position = _camTransform.forward * _distance;
        }

        var lookPos = _camTransform.transform.position + _camTransform.forward * _distance;
        _aimObject.transform.position = Vector3.Lerp(_aimObject.transform.position, lookPos, _speed * Time.deltaTime);
        _shootObject.transform.position = tempPos;//Vector3.Lerp(_aimObject.transform.position, tempPos, _speed * Time.deltaTime);
    }

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        if (_aimObject != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_aimObject.transform.position, _gizmoRadius);
        }

        if (_shootObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_shootObject.transform.position, _gizmoRadius);
        }

        if (_camTransform != null)
        {

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_camTransform.transform.position + _camTransform.forward * _distance, _gizmoRadius);
        }
    }

#endif

}

