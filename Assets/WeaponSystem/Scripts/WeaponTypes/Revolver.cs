using System.Collections;
using UnityEngine;
using WeaponSystem.Bullet;

namespace WeaponSystem
{
    public class Revolver : OneHandedWeapon
    {
        [SerializeField] private float _cockTime = .2f;
        private bool _cockedTrigger = true;
        private bool _cocking = false;
        private WaitForSeconds _cockWait;
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
            _cockWait = new WaitForSeconds(_cockTime);
        }

        public override void TryShoot()
        {
            if (_cocking) return;

            if (!_cockedTrigger)
            {
                StartCoroutine(CockTriggerRoutine());
                return;
            }

            if (!_canShoot) return;

            if (_bulletCountInMagazine < 1)
            {
                Debug.Log("no ammo");
                return;
            }

            Shoot();
        }

        private IEnumerator CockTriggerRoutine()
        {
            _cocking = true;

            yield return _cockWait;

            Debug.Log("CockTrigger");
            _cockedTrigger = true;
            _cocking = false;
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
            _cockedTrigger = false;

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
            _cockedTrigger = true;
        }
    }
}