using UnityEngine;

namespace WeaponSystem.Bullet
{
    public class Shell : BaseAmmo
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _inaccuracyDistance = 25;
        [SerializeField] private int _bulletCount = 10;

        void Start()
        {
            for (int i = 0; i < _bulletCount; i++)
            {
                Vector3 targetPos = transform.forward * 10;
                targetPos.x += Random.Range(-_inaccuracyDistance, _inaccuracyDistance);
                targetPos.y += Random.Range(-_inaccuracyDistance, _inaccuracyDistance);
                targetPos.z += Random.Range(-_inaccuracyDistance, _inaccuracyDistance);

                Vector3 dir = targetPos - transform.position;

                Bullet b = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                //b.InitBullet(dir);
            }
        }
    }
}