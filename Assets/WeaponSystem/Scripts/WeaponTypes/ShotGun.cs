using System.Collections;
using UnityEngine;
using WeaponSystem.Bullet;

namespace WeaponSystem
{
    public class ShotGun : BaseWeapon
    {
        [SerializeField] private float _inaccuracyDistance = 1;
        [SerializeField] private int _bulletsInShell = 10;

        public override int Type => WeaponTypes.TwoHand;

        public override void Init()
        {
            var components = GetComponentsInChildren<IInitializable>();

            foreach (var component in components)
            {
                if (component == (IInitializable)this)
                {
                    continue;
                }
                component.Init();
            }

            _lookPointTransform = ServiceLocator.Current.Get<CamerasHolder>().ShootTarget.transform;
        }

        public override void TryShoot()
        {
            if (!_canShoot) return;

            if (_bulletCountInMagazine < 1)
            {
                Debug.Log("no ammo");
                return;
            }

            Shoot();
        }

        public override void Shoot()
        {
            ShootShell();
            _bulletCountInMagazine--;
            ShotEvent?.Invoke();
            _canShoot = false;

            if (_pauseRoutine == null)
            {
                _pauseRoutine = PauseBetweenShots();
                StartCoroutine(_pauseRoutine);
            }
        }
        private IEnumerator PauseBetweenShots()
        {
            yield return new WaitForSeconds(_timeBetweenShots);
            _canShoot = true;
            _pauseRoutine = null;
        }

        public override void Reload()
        {
            _bulletCountInMagazine = _maxBulletsPerMagazine;
            _canShoot = true;

        }

        private void ShootShell()
        {
            for (int i = 0; i < _bulletsInShell; i++)
            {
                Vector3 direction = _lookPointTransform.position - _shootPoint.position;
                Vector3 targetPos = _shootPoint.position + direction.normalized * 10;

                targetPos = new Vector3(
                targetPos.x + Random.Range(-_inaccuracyDistance, _inaccuracyDistance),
                targetPos.y + Random.Range(-_inaccuracyDistance, _inaccuracyDistance),
                targetPos.z + Random.Range(-_inaccuracyDistance, _inaccuracyDistance)
                );

                var b = BulletPool.Instance.GetPooledObject();

                b.gameObject.SetActive(true);
                b.transform.position = _shootPoint.position;
                b.transform.rotation = _shootPoint.rotation;
                b.transform.LookAt(targetPos);
            }
        }
    }
}