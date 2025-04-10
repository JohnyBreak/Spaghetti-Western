using Cinemachine;
using WeaponSystem;
using UnityEngine;

[RequireComponent (typeof(BaseWeapon))]
[RequireComponent(typeof(CinemachineImpulseSource))]
public class Recoil : MonoBehaviour, IInitializable
{
    //[SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private CinemachineImpulseSource _cameraShake;
    [SerializeField] private float _verticalRecoil = 10f;
    [SerializeField] private float _duration = 0.1f;

    private BaseWeapon _gun;
    private Transform _mainCameraTransform;
    private CinemachinePOV _cameraPOV;
    private float _time;
    
    public void Init()
    {
        _gun = GetComponent<BaseWeapon>();
        _gun.ShotEvent += GenerateRecoil;
        if(_cameraShake == null) _cameraShake = GetComponent<CinemachineImpulseSource>();
        _mainCameraTransform = Camera.main.transform;

        _cameraPOV = ServiceLocator.Current
            .Get<CamerasHolder>().RegularCamera
            .GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachinePOV>();
    }

    private void OnDestroy()
    {
        _gun.ShotEvent -= GenerateRecoil;
    }

    public void GenerateRecoil() 
    {
        _time = _duration;
        
        _cameraShake.GenerateImpulse(_mainCameraTransform.forward);
    }

    void Update()
    {
        if (_time > 0) 
        {
            _cameraPOV.m_VerticalAxis.Value -=((_verticalRecoil /1000) * Time.deltaTime) / _time;
            _time -= Time.deltaTime;
        }
    }
}
