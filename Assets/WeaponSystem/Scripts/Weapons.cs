using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace WeaponSystem
{
    public class Weapons : MonoBehaviour, IInitializable
    {
        [SerializeField] private WeaponLibrary _library;
        [SerializeField] private UnitBattleStateController _battleState;
        [SerializeField] private GameObject _holder1;
        [SerializeField] private GameObject _holder2;

        public Action WeaponsChanged;
        private PlayerInput _input;
        
        private BaseWeapon _pistol1;
        private BaseWeapon _pistol2;

        private GameObject[] _holders;

        private Dictionary<int, WeaponInfo> _infosMap = new();
        private Dictionary<int, List<BaseWeapon>> _weaposMap = new();

        private int _activeWeaponIndex = -1;

        private int[] _availableWeapons = new int[]
        {
            0,1,2,3,4,5,6
        };

        public bool TwoHands => IsTwoHands();

        public bool HasActiveGun => _activeWeaponIndex >= 0;

        public void Init()
        {
            _input = ServiceLocator.Current.Get<PlayerInput>();
            _input.LMBPressEvent += OnLMBPress;
            _input.LMBReleaseEvent += OnLMBRelease;
            _input.ReloadEvent += OnReload;
            //_input.NumberPressEvent += OnNumberPress;
            _input.MouseScrollEvent += ScrollWeapon;

            _holders = new GameObject[] { _holder1, _holder2 };


            for (int i = 0; i < _library.Infos.Length; i++)
            {
                _infosMap[i] = _library.Infos[i];
            }

            foreach (var index in _availableWeapons)
            {
                if (_infosMap.TryGetValue(index, out var info)) 
                {
                    _weaposMap[index] = new List<BaseWeapon>();
                    
                    for (int i = 0; i < info.Amount; i++)
                    {
                        var weapon =  Instantiate(info.Prefab, _holders[i].transform);
                        
                        weapon.Init();
                        weapon.gameObject.SetActive(false);
                        
                        _weaposMap[index].Add(weapon);
                    }
                }
            }

            SelectWeapon(1);
        }

        public int GetCurrentWeaponType() 
        {
            if (_activeWeaponIndex < 0) 
            {
                return -1;
            }

            if (_infosMap.TryGetValue(_activeWeaponIndex, out var info)) 
            {
                return info.Type;
            }

            return -1;
        }

        private void OnDestroy()
        {
            _input.LMBPressEvent -= OnLMBPress;
            _input.LMBReleaseEvent -= OnLMBRelease;
            _input.ReloadEvent -= OnReload;
            //_input.NumberPressEvent -= OnNumberPress;
            _input.MouseScrollEvent -= ScrollWeapon;
        }

        private bool IsTwoHands()
        {
            if (_infosMap.TryGetValue(_activeWeaponIndex, out var info))
            {
                return info.Amount > 1;
            }

            return false;
        }

        private void OnReload(bool obj)
        {
            if (_weaposMap.TryGetValue(_activeWeaponIndex, out var weapons) == false)
            {
                return;
            }

            foreach (var gun in weapons)
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

            if (_weaposMap.TryGetValue(_activeWeaponIndex, out var weapons) == false)
            {
                return;
            }

            foreach (var gun in weapons)
            {
                gun.TryShoot();
            }

        }

        private void OnLMBRelease()
        {
            if (!HasActiveGun) return;

            if (_weaposMap.TryGetValue(_activeWeaponIndex, out var weapons) == false)
            {
                return;
            }

            foreach (var gun in weapons)
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
                    SelectWeapon(0);
                    //SetActiveWeapon(new BaseWeapon[] { _pistol1 });
                    break;
                case 2:
                    SelectWeapon(1);
                    //SetActiveWeapon(new BaseWeapon[] { _pistol1, _pistol2 });
                    break;
                case 3:
                    SelectWeapon(2);
                    //SetActiveWeapon(new BaseWeapon[] { });
                    break;
            }
        }

        private void SelectWeapon(int index) 
        {
            if (_weaposMap.TryGetValue(index, out var weapons)) 
            {
                foreach (var gun in weapons)
                {
                    gun.gameObject.SetActive(true);
                }

                if (_weaposMap.TryGetValue(_activeWeaponIndex, out var oldWeapons))
                {
                    foreach (var gun in oldWeapons)
                    {
                        gun.gameObject.SetActive(false);
                    }
                }

                _activeWeaponIndex = index;
            }
        }

        private void ScrollWeapon(int scrollDir) //1 up, -1 down
        {
            int tempIndex = 0;
            switch (scrollDir)
            {
                case 1:
                    tempIndex = _activeWeaponIndex + 1;

                    if (tempIndex >= _weaposMap.Count) 
                    {
                        tempIndex = 0;
                    }

                    SelectWeapon(tempIndex);
                    break;
                case -1:
                    tempIndex = _activeWeaponIndex - 1;

                    if (tempIndex < 0)
                    {
                        tempIndex = _weaposMap.Count - 1;
                    }
                    SelectWeapon(tempIndex);
                    break;
            }
        }

        //private void SetActiveWeapon(BaseWeapon[] _guns)
        //{
        //    _activeGuns.Clear();
        //    _activeGuns.Capacity = 2;

        //    for (int i = 0; i < _guns.Length; i++)
        //    {
        //        if (_guns[i] == null) continue;

        //        _activeGuns.Add(_guns[i]);
        //    }

        //    WeaponsChanged?.Invoke();
        //}
    }
}