using UnityEngine;

namespace WeaponSystem
{
    public class WeaponSwitcher : MonoBehaviour
    {
        private BaseWeapon[] _guns;

        private int _currentGunIndex = 0;

        private void Awake()
        {
            _guns = GetComponentsInChildren<BaseWeapon>(true);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                _currentGunIndex--;
                if (_currentGunIndex < 0) _currentGunIndex = _guns.Length - 1;
                SwitchGun(_currentGunIndex);
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                _currentGunIndex++;
                if (_currentGunIndex > _guns.Length - 1) _currentGunIndex = 0;
                SwitchGun(_currentGunIndex);
            }
        }

        private void SwitchGun(int index)
        {
            foreach (var item in _guns)
            {
                item.gameObject.SetActive(false);
            }
            _guns[index].gameObject.SetActive(true);
        }

    }
}