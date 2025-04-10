using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponLibrary", menuName = "GunConfigs/WeaponLibrary")]
    public class WeaponLibrary : ScriptableObject
    {
        [SerializeField] private WeaponInfo[] _infos;

        public WeaponInfo[] Infos => _infos;
    }
}