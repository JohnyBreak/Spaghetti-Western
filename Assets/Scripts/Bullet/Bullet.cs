using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Bullet : BaseAmmo, IPoolable
{
    public event Action EnableEvent;
    [SerializeField] private float _lifeTime = 3;
    private float _timer = 0;
    [SerializeField] private BulletType _type;
    public BulletType Type => _type;

    public void BackToPool()
    {
        BulletPool.Instance.BackObjectToPool(this);
    }

    //private Vector3 _lastPosition;

    //[SerializeField] private LayerMask _mask;

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

    //private void OnEnable()
    //{
    //    StartCoroutine(MoveRoutine());
    //    StartCoroutine(DisableRoutine());
    //}

    //public void InitBullet()//Vector3 dir) 
    //{
    //    //transform.LookAt(dir);

    //    //StartCoroutine(MoveRoutine());
    //}

    //private IEnumerator DisableRoutine()
    //{
    //    yield return new WaitForSeconds(2.5f);
    //    //Destroy(gameObject);
    //    StopAllCoroutines();
    //    BulletPool.Instance.DisableObject(this.gameObject);
    //}

    //private IEnumerator MoveRoutine() 
    //{
    //    _lastPosition = transform.position;
    //    RaycastHit hit;

    //    while (true)
    //    {
    //        yield return null;
    //        transform.position += transform.forward * Time.deltaTime * _speed;

    //        if (Physics.Linecast(_lastPosition, transform.position, out hit, _mask)) 
    //        {
    //            Debug.LogError(hit.collider);

    //            BulletPool.Instance.DisableObject(this.gameObject);
    //        }
    //        _lastPosition = transform.position;
    //    }

    //}

    //private void OnDisable()
    //{
    //    StopAllCoroutines();
    //}
}
