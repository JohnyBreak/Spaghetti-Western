using UnityEngine;
using WeaponSystem;

public class PlayerBattleRig : MonoBehaviour
{
    [SerializeField] private PlayerAnimation _animation;
    [SerializeField] private UnitRig _rig;
    [SerializeField] private UnitBattleStateController _battleState;
    [SerializeField] private Weapons _weapons;
    [SerializeField] private WeaponHolder _rightWeaponHolder;
    [SerializeField] private WeaponHolder _leftWeaponHolder;

    void Awake()
    {
        _weapons.WeaponsChanged += OnWeaponUpdate;
        _battleState.BattleStateChangedEvent += OnBattleStateChanged;
    }

    private void OnDestroy()
    {
        _weapons.WeaponsChanged -= OnWeaponUpdate;
        _battleState.BattleStateChangedEvent -= OnBattleStateChanged;
    }


    private void OnWeaponUpdate() 
    {
        OnBattleStateChanged(_battleState.CurrentState);
    }

    private void OnBattleStateChanged(int state)
    {
        switch (state)
        {
            case BattleState.Regular:

                ToggleRightHand(false);
                ToggleLeftHand(false);
                _rig.ToggleRig(RigIndexes.Spine, false);
                //_rig.ToggleTwoHandAimIKRig(false);

                _rig.ToggleRig(RigIndexes.TwoHandedAim, false);
                if (_weapons.TwoHanded)
                {
                    _rig.ToggleRig(RigIndexes.SingleHandedWeaponIdle, false, 0);
                    if (_weapons.GetCurrentWeapons().Count < 1)
                    {
                        break;
                    }

                    _rightWeaponHolder.SetWeapon(_weapons.GetCurrentWeapons()[0], false, 0);
                }
                else 
                {
                    _rig.ToggleRig(RigIndexes.SingleHandedWeaponIdle, true, 0);
                    if (_weapons.HasActiveGun) 
                    {
                        _rightWeaponHolder.SetWeapon(_weapons.GetCurrentWeapons()[0], false, 0);
                    }
                    if (_weapons.TwoHands) 
                    {
                        _leftWeaponHolder.SetWeapon(_weapons.GetCurrentWeapons()[1], false, 1);
                    }
                }
                //_rig.ToggleTwoHandIdleIKRig(_weapons.TwoHanded);

                _rig.ToggleRig(RigIndexes.TwoHandedIdle, _weapons.TwoHanded);
                break;
            case BattleState.Ready:

            //    _rig.ToggleSpineRig(false);
            //    if (!_weapons.HasActiveGun) break;
            //    _rig.ToggleRightHolsterRig(true);
            //    _rig.ToggleRightIKRig(false);
            //    _rig.ToggleRightAimRig(false);

            //    if (_weapons.TwoHands)
            //    {
            //        _rig.ToggleLeftHolsterRig(true);
            //        _rig.ToggleLeftIKRig(false);
            //        _rig.ToggleLeftAimRig(false);
            //    }

            //    break;
            case BattleState.Aim:

                _rig.ToggleRig(RigIndexes.Spine, true);
                //_rig.ToggleTwoHandIdleIKRig(false);
                
                _rig.ToggleRig(RigIndexes.TwoHandedIdle, false);
                if (!_weapons.HasActiveGun)
                {
                    ToggleRightHand(false);
                    ToggleLeftHand(false);
                    //_rig.ToggleTwoHandAimIKRig(false);

                    _rig.ToggleRig(RigIndexes.TwoHandedAim, false);
                    break;
                }

                _rightWeaponHolder.SetWeapon(_weapons.GetCurrentWeapons()[0], true, 0);


                if (_weapons.TwoHanded)
                {

                    if (_weapons.GetCurrentWeapons().Count < 1)
                    {
                        break;
                    }

                    //_rig.ToggleTwoHandAimIKRig(true);
                    _rig.ToggleRig(RigIndexes.SingleHandedWeaponIdle, false, 0);
                    _rig.ToggleRig(RigIndexes.TwoHandedAim, true);
                    break;
                }
                else 
                {
                    //_rig.ToggleTwoHandAimIKRig(false);
                    _rig.ToggleRig(RigIndexes.SingleHandedWeaponIdle, true, 0);
                    _rig.ToggleRig(RigIndexes.TwoHandedAim, false);
                    ToggleRightHand(_weapons.HasActiveGun);

                    if (_weapons.TwoHands)
                    {
                        _leftWeaponHolder.SetWeapon(_weapons.GetCurrentWeapons()[1], true, 1);
                    }
                    ToggleLeftHand(_weapons.TwoHands);
                }
                
                break;
        }

    }


    private void ToggleRightHand(bool isActive) 
    {
        _animation.SetLayerWeight(3, (isActive) ? 1 : 0);
        //_rig.ToggleRightHolsterRig(!isActive);
        //_rig.ToggleRightIKRig(isActive);
        _rig.ToggleRig(RigIndexes.RightHand, isActive);
        //_rig.ToggleRightAimRig(isActive);
    }

    private void ToggleLeftHand(bool isActive) 
    {
        _animation.SetLayerWeight(4, (isActive) ? 1 : 0);
        //_rig.ToggleLeftHolsterRig(!isActive);
        //_rig.ToggleLeftIKRig(isActive);
        _rig.ToggleRig(RigIndexes.LeftHand, isActive);
        //_rig.ToggleLeftAimRig(isActive);
    }
}
