using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : BaseGun
{
    //void Update()
    //{
    //    Debug.DrawRay(_shootPoint.position, _shootPoint.forward * 50f, Color.red);

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        Reload();
    //    }


    //    if (Input.GetKeyDown(KeyCode.Mouse0)) 
    //    {
    //        Shoot();
    //    }
    //}

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
        //Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);//.InitBullet();

        var b = BulletPool.Instance.GetPooledObject();
        b.gameObject.SetActive(true);
        b.transform.position = _shootPoint.position;
        //b.transform.rotation = _shootPoint.rotation;
        b.transform.LookAt(_lookPointTransform.position);

        _bulletCountInMagazine--;
        _canShoot = false;

        if(_pauseRoutine == null)
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
}
