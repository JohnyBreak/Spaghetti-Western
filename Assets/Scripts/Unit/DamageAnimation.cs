using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimation : MonoBehaviour
{
    [SerializeField] private UnitAnimation _animation;
    [SerializeField] private HealthSystem _healthSystem;
    // Start is called before the first frame update
    void Awake()
    {
        _healthSystem.DamageEvent += OnDamage;
    }
    
    private void OnDamage() 
    {
        _animation.TriggerDamage();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        _healthSystem.DamageEvent -= OnDamage;
    }
}
