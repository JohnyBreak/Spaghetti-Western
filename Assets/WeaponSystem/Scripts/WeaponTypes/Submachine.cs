using System.Collections;
using UnityEngine;
using WeaponSystem.Bullet;

namespace WeaponSystem
{
    public class Submachine : BaseWeapon
    {
        private bool _isShooting = false;
        private IEnumerator _shootingRoutine;
        public override int Type => WeaponTypes.OneHand;

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

            StartShoot();
        }

        private void StartShoot()
        {
            _isShooting = true;
            Shoot();
        }

        public void StopShoot()
        {
            _isShooting = false;
        }

        public override void Shoot()
        {
            var b = BulletPool.Instance.GetPooledObject();
            b.gameObject.SetActive(true);
            b.transform.position = _shootPoint.position;
            b.transform.LookAt(_lookPointTransform.position);

            _bulletCountInMagazine--;

            ShotEvent?.Invoke();

            _canShoot = false;
            _pauseRoutine = PauseBetweenShots();
            StartCoroutine(_pauseRoutine);
        }

        private IEnumerator PauseBetweenShots()
        {
            yield return new WaitForSeconds(_timeBetweenShots);
            _canShoot = true;
            if (_isShooting) TryShoot();
        }

        public override void Reload()
        {
            _bulletCountInMagazine = _maxBulletsPerMagazine;
            _canShoot = true;

        }
    }
}