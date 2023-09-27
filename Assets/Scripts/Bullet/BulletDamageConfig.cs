using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BulletDamageConfig", menuName = "GunConfigs/Bullet/BulletDamageConfig")]
public class BulletDamageConfig : ScriptableObject
{
    public List<BulletDamageData> BulletDamageDatas;
}

[System.Serializable]
public class BulletDamageData 
{
    public BulletType Type;
    public int Damage;
}
