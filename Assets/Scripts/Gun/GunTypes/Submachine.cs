using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submachine : BaseGun
{
    private bool _isShooting = false;
    private IEnumerator _shootingRoutine;

    void Update()
    {
        Debug.DrawRay(_shootPoint.position, _shootPoint.forward * 50f, Color.red);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryShoot();
        }



        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopShoot();
        }
    }

    private void TryShoot()
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

    private void StopShoot()
    {
        _isShooting = false;

        //if (_shootingRoutine != null)
        //{
        //    StopCoroutine(_shootingRoutine);
        //    _shootingRoutine = null;
        //}
    }

    public override void Shoot()
    {
        //Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);//.InitBullet();

        var b = BulletPool.Instance.GetPooledObject();
        b.gameObject.SetActive(true);
        b.transform.position = _shootPoint.position;
        //b.transform.rotation = _shootPoint.rotation;
        b.transform.LookAt(_lookPointTransform.position);

        _bulletCountInMagazine--;

        ShotEvent?.Invoke();

        _canShoot = false;
        //if(_pauseRoutine == null) 
        _pauseRoutine = PauseBetweenShots();
        StartCoroutine(_pauseRoutine);
    }

    private IEnumerator PauseBetweenShots()
    {
        yield return new WaitForSeconds(_timeBetweenShots);
        _canShoot = true;
        if (_isShooting) TryShoot();
    }

    //private IEnumerator ShootingRoutine()
    //{
    //    yield return null;

    //    while (_isShooting && _bulletCountInMagazine > 0)
    //    {
    //        Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation).InitBullet();
    //        _bulletCountInMagazine--;
    //        _canShoot = false;

    //        yield return new WaitForSeconds(_timeBetweenShots);
    //        _canShoot = true;
    //    }

    //    StopShoot();
    //}

    public override void Reload()
    {
        _bulletCountInMagazine = _maxBulletsPerMagazine;
        _canShoot = true;

    }
}
