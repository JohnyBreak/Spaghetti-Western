using UnityEngine;

public class UnitBattleRig : MonoBehaviour
{
    [SerializeField] private UnitRig _rig;
    [SerializeField] private UnitBattleStateController _battleState;
    [SerializeField] private Weapons _weapons;
    //[SerializeField] private bool _twoHandRigs;

    void Awake()
    {
        _battleState.BattleStateChangedEvent += OnBattleStateChanged;
    }

    private void OnDestroy()
    {
        _battleState.BattleStateChangedEvent -= OnBattleStateChanged;
    }

    private void OnBattleStateChanged(string state)
    {
        switch (state)
        {
            case BattleState.Regular:
                _rig.ToggleSpineRig(false);
                if (!_weapons.HasActiveGun) break;
                _rig.ToggleRightHolsterRig(true);
                _rig.ToggleRightIKRig(false);
                _rig.ToggleRightAimRig(false);

                if (_weapons.TwoHands)
                {
                    _rig.ToggleLeftHolsterRig(true);
                    _rig.ToggleLeftIKRig(false);
                    _rig.ToggleLeftAimRig(false);
                }

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
                if (!_weapons.HasActiveGun) break;

                _rig.ToggleRightHolsterRig(false);
                _rig.ToggleRightIKRig(true);
                _rig.ToggleRightAimRig(true);

                if (_weapons.TwoHands)
                {
                    _rig.ToggleLeftHolsterRig(false);
                    _rig.ToggleLeftIKRig(true);
                    _rig.ToggleLeftAimRig(true);
                }

                break;
        }
    }
}
