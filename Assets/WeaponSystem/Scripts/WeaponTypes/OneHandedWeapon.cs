using System;
using UnityEngine;
namespace WeaponSystem
{
    public abstract class OneHandedWeapon : BaseWeapon
    {
        [SerializeField] protected IKSettingsPair[] _pairs;
        public IKSettingsPair[] IKSettingsPairs => _pairs;
    }

    [Serializable]
    public class IKSettingsPair 
    {
        [SerializeField] protected OneHandedWeaponIKSettings _idleIKSettings;
        [SerializeField] protected OneHandedWeaponIKSettings _aimIKSettings;

        public OneHandedWeaponIKSettings IdleIKSettings => _idleIKSettings;
        public OneHandedWeaponIKSettings AimIKSettings => _aimIKSettings;
    }
}