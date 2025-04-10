using System;
using UnityEngine;
namespace WeaponSystem.Bullet
{
    public class Bullet : BaseAmmo, IPoolable
    {
        public event Action EnableEvent;
        [SerializeField] private float _lifeTime = 3;
        private float _timer = 0;
        [SerializeField] private int _type;
        public int Type => _type;

        public void BackToPool()
        {
            BulletPool.Instance.BackObjectToPool(this);
        }

        private void OnEnable()
        {
            _timer = 0;
            EnableEvent?.Invoke();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _lifeTime)
            {
                BackToPool();
            }
        }
    }
}