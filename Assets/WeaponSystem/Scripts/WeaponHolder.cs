using UnityEngine;

namespace WeaponSystem
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightElbowHint;
        [SerializeField] private Transform _leftElbowHint;

        public void SetWeapon(BaseWeapon weapon, bool aiming, int handIndex)
        {
            if (weapon is OneHandedWeapon oneHandedWeapon)
            {
                
                var settings = aiming ? oneHandedWeapon.IKSettingsPairs[handIndex].AimIKSettings : oneHandedWeapon.IKSettingsPairs[handIndex].IdleIKSettings;

                _pivot.localPosition = settings.Pivot.Position;
                _pivot.localEulerAngles = settings.Pivot.Rotation;
                return;
            }

            if (weapon is TwoHandedWeapon twoHandedWeapon)
            {
                var settings = aiming ? twoHandedWeapon.AimIKSettings : twoHandedWeapon.IdleIKSettings;

                _pivot.localPosition = settings.Pivot.Position;
                _pivot.localEulerAngles = settings.Pivot.Rotation;

                _rightHand.localPosition = settings.RightHand.Position;
                _rightHand.localEulerAngles = settings.RightHand.Rotation;

                _leftHand.localPosition = settings.LeftHand.Position;
                _leftHand.localEulerAngles = settings.LeftHand.Rotation;

                _rightElbowHint.localPosition = settings.RightElbow.Position;
                _leftElbowHint.localEulerAngles = settings.LeftElbow.Rotation;
            }
        }
    }
}