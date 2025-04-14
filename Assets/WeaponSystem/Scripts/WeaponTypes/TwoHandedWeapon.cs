using UnityEngine;

namespace WeaponSystem
{
    public abstract class TwoHandedWeapon : BaseWeapon
    {
        [SerializeField] protected TwoHandedWeaponIKSettings _idleIKSettings;
        [SerializeField] protected TwoHandedWeaponIKSettings _aimIKSettings;

        public TwoHandedWeaponIKSettings IdleIKSettings => _idleIKSettings;
        public TwoHandedWeaponIKSettings AimIKSettings => _aimIKSettings;
    }
}