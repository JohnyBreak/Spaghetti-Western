using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class Weapons : MonoBehaviour, IInitializable
    {
        public Action WeaponsChanged;
        private PlayerInput _input;
        [SerializeField] private UnitBattleStateController _battleState;
        [SerializeField] private GameObject _holder1;
        [SerializeField] private GameObject _holder2;

        private BaseWeapon _pistol1;
        private BaseWeapon _pistol2;

        private BaseWeapon[] _guns;

        private List<BaseWeapon> _activeGuns = new List<BaseWeapon>();

        public bool TwoHands => _activeGuns.Count > 1;
        public bool HasActiveGun => _activeGuns.Count > 0;

        public void Init()
        {
            _input = ServiceLocator.Current.Get<PlayerInput>();
            _input.LMBPressEvent += OnLMBPress;
            _input.LMBReleaseEvent += OnLMBRelease;
            _input.ReloadEvent += OnReload;
            _input.NumberPressEvent += OnNumberPress;
        }

        private void Awake()
        {
            _pistol1 = _holder1.GetComponentInChildren<BaseWeapon>();
            _pistol2 = _holder2.GetComponentInChildren<BaseWeapon>();
        }

        private void OnDestroy()
        {
            _input.LMBPressEvent -= OnLMBPress;
            _input.LMBReleaseEvent -= OnLMBRelease;
            _input.ReloadEvent -= OnReload;
            _input.NumberPressEvent -= OnNumberPress;
        }

        private void OnReload(bool obj)
        {
            foreach (var gun in _activeGuns)
            {
                gun.Reload();
            }
        }

        private void OnLMBPress()
        {
            if (!HasActiveGun)
            {
                return;
            }

            if (_battleState.CurrentState == BattleState.Regular)
            {
                return;
            }

            foreach (var gun in _activeGuns)
            {
                gun.TryShoot();
            }

        }

        private void OnLMBRelease()
        {
            if (!HasActiveGun) return;

            foreach (var gun in _activeGuns)
            {
                if (gun is Submachine submachine)
                {
                    submachine.StopShoot();
                }
            }
        }

        private void OnNumberPress(int number)
        {
            switch (number)
            {
                case 1:
                    SetActiveWeapon(new BaseWeapon[] { _pistol1 });
                    break;
                case 2:
                    SetActiveWeapon(new BaseWeapon[] { _pistol1, _pistol2 });
                    break;
                case 3:
                    SetActiveWeapon(new BaseWeapon[] { });
                    break;
            }
        }

        private void SetActiveWeapon(BaseWeapon[] _guns)
        {
            _activeGuns.Clear();
            _activeGuns.Capacity = 2;

            for (int i = 0; i < _guns.Length; i++)
            {
                if (_guns[i] == null) continue;

                _activeGuns.Add(_guns[i]);
            }

            WeaponsChanged?.Invoke();
        }
    }
}