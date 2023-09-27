using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HitBox : MonoBehaviour
{
    private Rigidbody _rb;
    //private RagdollActivator _activator;
    private HealthSystem _healthSystem;
    private void Awake()
    {
        //_activator = GetComponentInParent<RagdollActivator>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;
    }

    //public void TakeDamage(int damage, Vector3 dir = new Vector3()) 
    //{
    //    //_activator.EnableRagdoll();
    //    _healthSystem.TakeDamage(damage);
    //    Debug.LogError(dir);
    //    //_rb.AddForce(dir, ForceMode.Impulse);
    //}

    public void TakeDamage(int damage, float force, RaycastHit hit, Vector3 dir = new Vector3())
    {
        _healthSystem.TakeDamage(damage, force, hit, dir);
        //_activator.EnableRagdoll();
        //Debug.LogError(dir * damage);
        _rb.AddForce(dir * force, ForceMode.Impulse);

        //Debug.Log($"AddForce {dir * force}");
    }
}
