using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DamageEvent
{
    public event Action<Damage> OnBeforeDamage;
    public event Action<Damage> OnTakeDamageTrigger;
    public event Action<Damage> DamgageDealtEvent;
    [ServerRpc]
    public void DealDamageServerRpc(Damage damage)
    {
        if (damage.DamageTaker.IsDead) return;
        OnBeforeDamage?.Invoke(damage);
        OnTakeDamageTrigger?.Invoke(damage);
        DamgageDealtEvent?.Invoke(damage);
    }
}
