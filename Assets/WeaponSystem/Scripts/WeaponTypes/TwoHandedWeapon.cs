using UnityEngine;

namespace WeaponSystem
{
    public abstract class TwoHandedWeapon : BaseWeapon
    {
        [SerializeField] protected TwoHandedWeaponIKSettings _iKSettings;

        public TwoHandedWeaponIKSettings IKSettings => _iKSettings;
    }
}