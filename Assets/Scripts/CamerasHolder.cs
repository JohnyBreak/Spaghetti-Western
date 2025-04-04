using UnityEngine;

public class CamerasHolder : MonoBehaviour, IService
{
    [SerializeField] private GameObject _regularCamera;
    [SerializeField] private GameObject _readyCamera;
    [SerializeField] private GameObject _aimCamera;

    public GameObject RegularCamera => _regularCamera;
    public GameObject ReadyCamera => _readyCamera;
    public GameObject AimCamera => _aimCamera;
}
