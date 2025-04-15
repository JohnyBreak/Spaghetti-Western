using System;
using UnityEngine;

namespace WeaponSystem
{
    [Serializable]
    public class OneHandedWeaponIKSettings
    {
        [SerializeField] private TransformSettings _pivot;
        public TransformSettings Pivot => _pivot;
    }
}