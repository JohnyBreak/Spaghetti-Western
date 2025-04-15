using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable, IHealable
{
    public event Action<int> HealthChangedEvent;
    public event Action DamageEvent;
    public event Action<RaycastHit> DamageHitEvent;
    public event Action HealEvent;
    public event Action DeathEvent;

    [SerializeField] private int _health = 100;

    private RagdollActivator _activator;
    private HitBox[] _hitboxes;
    //[HideInInspector] public CapsuleCollider Collider;

    //private UIManager _manager;

    //[Inject]
    //private void Construct(UIManager manager)
    //{
    //    _manager = manager;
    //}


    private void Awake()
    {
        _activator = GetComponentInParent<RagdollActivator>();
        _hitboxes = GetComponentsInChildren<HitBox>();

        foreach (HitBox hitbox in _hitboxes) 
        {
            hitbox.Init(this);
        }
        //Collider = GetComponent<CapsuleCollider>();
        //Collider.enabled = true;
    }

    public void SetHealth(int health) 
    {
        _health = health;
        HealthChangedEvent?.Invoke(_health);
    }

    public void TakeDamage(int damage, float force, RaycastHit hit, Vector3 dir = new Vector3())
    {
        DamageHitEvent?.Invoke(hit);

        if (_health <= 0) return;

        _health -= damage;

        if (_health <= 0)
        {
            DeathEvent?.Invoke();
            _activator.EnableRagdoll();
            Debug.Log("death");
            _health = 0;
        }
        else
        {
            DamageEvent?.Invoke();
            
            Debug.Log($"TakeDamage {damage}");
        }
        HealthChangedEvent?.Invoke(_health);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            DeathEvent?.Invoke();

            _activator.EnableRagdoll();
            _health = 0;
        }
        else
        {
            DamageEvent?.Invoke();
        }
        HealthChangedEvent?.Invoke(_health );
    }

    public void TakeHeal(int value)
    {
        HealEvent?.Invoke();

        _health += value;
        HealthChangedEvent?.Invoke(_health);
    }
}
