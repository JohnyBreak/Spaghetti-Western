using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BaseGun))]
[RequireComponent(typeof(CinemachineImpulseSource))]
public class Recoil : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _camera;
    [SerializeField] private Cinemachine.CinemachineImpulseSource _cameraShake;
    [SerializeField] private float _verticalRecoil = 10f;
    [SerializeField] private float _duration = 0.1f;

    private BaseGun _gun;
    private Transform _mainCameraTransform;
    private CinemachinePOV _cameraPOV;
    private float _time;
    
    void Awake()
    {
        _gun = GetComponent<BaseGun>();
        _gun.ShotEvent += GenerateRecoil;
        if(_cameraShake == null) _cameraShake = GetComponent<CinemachineImpulseSource>();
        _mainCameraTransform = Camera.main.transform;
        _cameraPOV = _camera.GetCinemachineComponent<CinemachinePOV>();
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
