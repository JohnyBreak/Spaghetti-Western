using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponLibrary", menuName = "GunConfigs")]
    public class WeaponLibrary : ScriptableObject
    {
        [SerializeField] private WeaponInfo _info;
    }
}