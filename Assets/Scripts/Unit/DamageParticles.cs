using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageParticles : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    private BloodParticlePool _pool;

    void Start()
    {
        _pool = (BloodParticlePool) BloodParticlePool.Instance;
        _healthSystem.DamageHitEvent += OnDamage;
    }

    private void OnDamage(RaycastHit hit)
    {
        var particle = _pool.GetPooledObject();
        particle.transform.position = hit.point;
        particle.transform.rotation = Quaternion.FromToRotation(particle.transform.forward, hit.normal);
        particle.gameObject.SetActive(true);
        particle.Play();
    }

    void OnDestroy()
    {
        _healthSystem.DamageHitEvent -= OnDamage;
    }
}
