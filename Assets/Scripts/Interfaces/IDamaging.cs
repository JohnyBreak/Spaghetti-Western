using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamaging
{
    int Damage { get; set; }

    void GiveDamage(IDamageable damagable);
}
