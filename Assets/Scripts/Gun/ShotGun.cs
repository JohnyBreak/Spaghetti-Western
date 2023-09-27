using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : BaseGun
{
    [SerializeField] private float _inaccuracyDistance = 1;
    [SerializeField] private int _bulletsInShell = 10;

    void Start()
    {

    }
    void Update()
    {
        Debug.DrawRay(_shootPoint.position, _shootPoint.forward * 50f, Color.red);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        if (!_canShoot) return;

        if (_bulletCountInMagazine < 1)
        {
            Debug.Log("no ammo");
            return;
        }

        //Instantiate(_bulletPrefab, _shootPoint.position, transform.localRotation);
        ShootShell();
        _bulletCountInMagazine--;
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
            //Vector3 targetPos = _lookPointTransform.position;
            Vector3 targetPos = _shootPoint.position + _shootPoint.forward * 10;

            targetPos = new Vector3(
            targetPos.x + Random.Range(-_inaccuracyDistance, _inaccuracyDistance),
            targetPos.y + Random.Range(-_inaccuracyDistance, _inaccuracyDistance),
            targetPos.z + Random.Range(-_inaccuracyDistance, _inaccuracyDistance)
            );

            //Bullet b = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
            var b = BulletPool.Instance.GetPooledObject();

            b.gameObject.SetActive(true);
            b.transform.position = _shootPoint.position;
            b.transform.rotation = _shootPoint.rotation;
            b.transform.LookAt(targetPos);
        }
    }
}
