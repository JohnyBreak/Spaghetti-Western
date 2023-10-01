using System;
using System.Collections;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour, IGun
{
    public Action ShotEvent;
    //[SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] protected int _maxBulletsPerMagazine;

    [SerializeField] protected float _timeBetweenShots = 0.1f;
    [SerializeField] protected Transform _shootPoint;
    [SerializeField] protected Transform _lookPointTransform;

    protected int _bulletCountInMagazine;
    protected bool _canShoot;
    protected IEnumerator _pauseRoutine;
    public abstract void Shoot();
    public abstract void TryShoot();

    public abstract void Reload();

    protected void Update()
    {
        Debug.DrawRay(_shootPoint.position, _shootPoint.forward * 50f, Color.red);
    }
}
