using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class BulletTrail : MonoBehaviour
{
    private TrailRenderer _trail;
    private Bullet _bullet;
    private float _ClearTimeS = float.NaN;

    void Awake()
    {
        _bullet = GetComponentInParent<Bullet>();
        _trail = GetComponent<TrailRenderer>();
        _bullet.EnableEvent += OnBulletEnable;
    }

    void OnRenderObject()
    {
        if (!float.IsNaN(_ClearTimeS))
        {
            _trail.time = _ClearTimeS;
            _ClearTimeS = float.NaN;
            _trail.Clear(); 
        }
    }

    private void OnBulletEnable()
    {
        if (!float.IsNaN(_ClearTimeS)) return;
        _ClearTimeS = _trail.time;
        _trail.time = 0;
    }

    private void OnDestroy()
    {
        _bullet.EnableEvent -= OnBulletEnable;
    }
}
