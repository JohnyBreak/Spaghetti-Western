using System;
using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponTypes
    {
        public const int OneHandMelee = 0;
        public const int OneHand = 1;
        public const int TwoHand = 2;
    }

    public abstract class BaseWeapon : MonoBehaviour, IWeapon, IInitializable
    {
        public Action ShotEvent;
        [SerializeField] protected int _maxBulletsPerMagazine;

        [SerializeField] protected float _timeBetweenShots = 0.1f;
        [SerializeField] protected Transform _shootPoint;
        [SerializeField] protected Transform _lookPointTransform;

        protected int _bulletCountInMagazine;
        protected bool _canShoot;
        protected IEnumerator _pauseRoutine;

        public abstract int Type { get; }
        public abstract void Shoot();
        public abstract void TryShoot();

        public abstract void Reload();

        public abstract void Init();

        //protected void Update()
        //{
        //    Debug.DrawRay(_shootPoint.position, _shootPoint.forward * 50f, Color.red);
        //    Debug.DrawLine(_shootPoint.position, _lookPointTransform.position, Color.green);
        //}
    }
}