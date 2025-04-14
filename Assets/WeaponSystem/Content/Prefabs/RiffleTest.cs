using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RiffleTest : MonoBehaviour
{
    [SerializeField] private bool _active;

    [SerializeField] private Transform _handRParent;

    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightElbowHint;
    [SerializeField] private Transform _leftElbowHint;
    [SerializeField, Range(0, 1)] private float _rigWeight;
    [SerializeField] private Rig _rig;

    [SerializeField] private Transform _clavice;
    [SerializeField] private Transform _weaponHolder;
    [SerializeField] private Transform _weaponPos;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _rotation;

    [SerializeField] private Transform _weaponRightHand;
    [SerializeField] private Transform _weaponLeftHand;
    [SerializeField] private Transform _weaponRightElbowHint;
    [SerializeField] private Transform _weaponLeftElbowHint;




    private void Update()
    {
        if (_active == false) 
        {
            _weaponHolder.parent = _handRParent;
            //_rig.weight = 0;
            return;
        }

        _rig.weight = _rigWeight;


        _weaponPos.position = _clavice.position + _offset;
        //_weaponHolder.eulerAngles = _rotation;

        _weaponHolder.parent = _weaponPos;



        _rightHand.position = _weaponRightHand.position;
        _leftHand.position = _weaponLeftHand.position;
        _rightElbowHint.position = _weaponRightElbowHint.position;
        _leftElbowHint.position = _weaponLeftElbowHint.position;

        _rightHand.rotation = _weaponRightHand.rotation;
        _leftHand.rotation = _weaponLeftHand.rotation;
        _rightElbowHint.rotation = _weaponRightElbowHint.rotation;
        _leftElbowHint.rotation = _weaponLeftElbowHint.rotation;
    }
}
