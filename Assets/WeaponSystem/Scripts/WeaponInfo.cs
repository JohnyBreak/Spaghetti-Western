using UnityEngine;

namespace WeaponSystem
{

    [CreateAssetMenu(fileName = "WeaponLibrary", menuName = "WeaponInfo")]
    public class WeaponInfo : ScriptableObject
    {
        [SerializeField] private BaseWeapon _weapon;
    }
}