using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Bullet
{
    [CreateAssetMenu(fileName = "BulletDamageConfig", menuName = "GunConfigs/Bullet/BulletDamageConfig")]
    public class BulletDamageConfig : ScriptableObject
    {
        public List<BulletDamageData> BulletDamageDatas;
    }

    [System.Serializable]
    public class BulletDamageData
    {
        public int Type;
        public int Damage;
    }
}