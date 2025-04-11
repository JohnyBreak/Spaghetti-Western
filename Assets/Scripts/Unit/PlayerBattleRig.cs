using UnityEngine;
using WeaponSystem;

public class PlayerBattleRig : MonoBehaviour
{
    [SerializeField] private PlayerAnimation _animation;
    [SerializeField] private UnitRig _rig;
    [SerializeField] private UnitBattleStateController _battleState;
    [SerializeField] private Weapons _weapons;

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
                _rig.ToggleSpineRig(false);
                ToggleRightHand(false);
                ToggleLeftHand(false);
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

                _rig.ToggleSpineRig(true);

                if (!_weapons.HasActiveGun)
                {
                    ToggleRightHand(false);
                    ToggleLeftHand(false);
                    break;
                }

                ToggleRightHand(_weapons.HasActiveGun);

                ToggleLeftHand(_weapons.TwoHands);

                break;
        }

    }


    private void ToggleRightHand(bool isActive) 
    {
        _animation.SetLayerWeight(3, (isActive) ? 1 : 0);
        //_rig.ToggleRightHolsterRig(!isActive);
        _rig.ToggleRightIKRig(isActive);
        //_rig.ToggleRightAimRig(isActive);
    }

    private void ToggleLeftHand(bool isActive) 
    {
        _animation.SetLayerWeight(4, (isActive) ? 1 : 0);
        //_rig.ToggleLeftHolsterRig(!isActive);
        _rig.ToggleLeftIKRig(isActive);
        //_rig.ToggleLeftAimRig(isActive);
    }
}
