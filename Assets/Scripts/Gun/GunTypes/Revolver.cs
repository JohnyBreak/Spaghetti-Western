using System.Collections;
using UnityEngine;

public class Revolver : BaseGun
{
    [SerializeField] private float _cockTime = .2f;
    private bool _cockedTrigger = true;
    private bool _cocking = false;
    private WaitForSeconds _cockWait;

    private void Awake()
    {
        _cockWait = new WaitForSeconds(_cockTime);
    }

    //void Update()
    //{
    //    Debug.DrawRay(_shootPoint.position, _shootPoint.forward * 50f, Color.red);
    //    Debug.DrawLine(_shootPoint.position, _lookPointTransform.position, Color.blue);

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        Reload();
    //    }

    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        TryShoot();
    //    }
    //}

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
        //Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);//.InitBullet();

        var b = BulletPool.Instance.GetPooledObject();
        b.gameObject.SetActive(true);
        b.transform.position = _shootPoint.position;
        //b.transform.rotation = _shootPoint.rotation;
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