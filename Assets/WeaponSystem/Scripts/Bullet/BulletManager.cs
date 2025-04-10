using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Bullet
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private float _speed = 4;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private BulletDamageConfig _bulletDamageConfig;
        private Dictionary<int, int> _bulletDamage = new Dictionary<int, int>();
        private BulletPool _pool;

        //private bool _isRunning = false;

        private void Awake()
        {
            foreach (var data in _bulletDamageConfig.BulletDamageDatas)
            {
                _bulletDamage.Add(data.Type, data.Damage);
            }

        }

        private void Start()
        {
            _pool = (BulletPool)BulletPool.Instance;
            Debug.Log("BulletManager Awake");
        }

        private void FixedUpdate()
        {
            var delta = Time.deltaTime;
            RaycastHit hit;
            for (int i = 0; i < _pool.PooledObjects.Count; i++)
            {
                if (!_pool.PooledObjects[i].gameObject.activeInHierarchy) continue;

                Bullet bulet = _pool.PooledObjects[i].GetComponent<Bullet>();

                _pool.LastPositions[i] = _pool.PooledObjects[i].transform.position;

                _pool.PooledObjects[i].transform.position
                    += _pool.PooledObjects[i].transform.forward * delta * _speed;

                if (Physics.Linecast(_pool.LastPositions[i],
                    _pool.PooledObjects[i].transform.position, out hit, _mask))
                {
                    //Debug.LogError(hit.collider);
                    //HitBox hitBox;
                    if (hit.collider.TryGetComponent(out HitBox hitBox))
                    {
                        hitBox.TakeDamage(_bulletDamage[_pool.PooledObjects[i].Type], 10f, hit, _pool.PooledObjects[i].transform.forward);
                    }

                    _pool.BackObjectToPool(_pool.PooledObjects[i]);
                }
                _pool.LastPositions[i] = _pool.PooledObjects[i].transform.position;
            }
        }

    }

    public class BulletType
    {
        public const int Pistol = 0;
        public const int MachineGun = 1;
        public const int Shotgun = 2;
    }
}