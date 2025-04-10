using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponInfo", menuName = "GunConfigs/WeaponInfo")]
    public class WeaponInfo : ScriptableObject
    {
        [SerializeField] private BaseWeapon _weapon;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _rotation;

        public BaseWeapon Prefab => _weapon;
        public Vector3 Offset => _offset;
        public Vector3 Rotation => _rotation;
    }
}