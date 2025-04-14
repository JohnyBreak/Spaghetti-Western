using UnityEngine;

namespace WeaponSystem
{
    public class TwoHandWeaponHolder : MonoBehaviour
    {
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightElbowHint;
        [SerializeField] private Transform _leftElbowHint;

        public void SetWeapon(BaseWeapon weapon) 
        {
            if (weapon is TwoHandedWeapon twoHandedWeapon) 
            {
                _rightHand.position = twoHandedWeapon.IKSettings.RightHand.position;
                _rightHand.rotation = twoHandedWeapon.IKSettings.RightHand.rotation;

                _leftHand.position = twoHandedWeapon.IKSettings.LeftHand.position;
                _leftHand.rotation = twoHandedWeapon.IKSettings.LeftHand.rotation;

                _rightElbowHint.position = twoHandedWeapon.IKSettings.RightElbow.position;
                _leftElbowHint.position = twoHandedWeapon.IKSettings.LeftElbow.position;
            }
        }
    }
}