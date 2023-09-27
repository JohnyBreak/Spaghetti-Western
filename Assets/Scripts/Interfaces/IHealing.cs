using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealing
{
    int HealAmount { get; set; }

    void Heal(IHealable damagable);
}
