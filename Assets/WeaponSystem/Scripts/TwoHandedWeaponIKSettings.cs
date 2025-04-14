using System;
using UnityEngine;

namespace WeaponSystem
{
    [Serializable]
    public class TwoHandedWeaponIKSettings
    {
        [SerializeField] private TransformSettings _pivot;
        [SerializeField] private TransformSettings _rightHand;
        [SerializeField] private TransformSettings _leftHand;
        [SerializeField] private TransformSettings _rightElbowHint;
        [SerializeField] private TransformSettings _leftElbowHint;
        [SerializeField,Range(0,1)] private float _rightHandWeight;
        [SerializeField, Range(0,1)] private float _leftHandWeight;

        public TransformSettings Pivot => _pivot;
        public TransformSettings RightHand => _rightHand;
        public TransformSettings LeftHand => _leftHand;
        public TransformSettings RightElbow => _rightElbowHint;

        public TransformSettings LeftElbow => _leftElbowHint;
    }

    [Serializable]
    public class TransformSettings 
    {
        public Vector3 Position;
        public Vector3 Rotation;
    }
}