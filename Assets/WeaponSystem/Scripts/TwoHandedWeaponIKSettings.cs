using System;
using UnityEngine;

namespace WeaponSystem
{
    [Serializable]
    public class TwoHandedWeaponIKSettings
    {
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightElbowHint;
        [SerializeField] private Transform _leftElbowHint;
        [SerializeField,Range(0,1)] private float _rightHandWeight;
        [SerializeField, Range(0,1)] private float _leftHandWeight;

        public Transform RightHand => _rightHand;
        public Transform LeftHand => _leftHand;
        public Transform RightElbow => _rightElbowHint;

        public Transform LeftElbow => _leftElbowHint;

    }
}