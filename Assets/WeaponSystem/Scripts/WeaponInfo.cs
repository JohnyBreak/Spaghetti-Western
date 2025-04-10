using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponInfo", menuName = "GunConfigs/WeaponInfo")]
    public class WeaponInfo : ScriptableObject
    {
        [SerializeField] private BaseWeapon _weapon;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private int _type;
        [SerializeField, Range(1,2)] private int _amount;

        public BaseWeapon Prefab => _weapon;
        public Vector3 Offset => _offset;

        public int Type => _type;
        public int Amount => _amount;
    }
}