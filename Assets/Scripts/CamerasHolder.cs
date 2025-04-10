using UnityEngine;

public class CamerasHolder : MonoBehaviour, IService
{
    [SerializeField] private GameObject _regularCamera;
    [SerializeField] private GameObject _readyCamera;
    [SerializeField] private GameObject _aimCamera;
    [SerializeField] private GameObject _headAimTarget;
    [SerializeField] private GameObject _shootTarget;

    public GameObject RegularCamera => _regularCamera;
    public GameObject ReadyCamera => _readyCamera;
    public GameObject AimCamera => _aimCamera;
    public GameObject HeadAimTarget => _headAimTarget;
    public GameObject ShootTarget => _shootTarget;
}
