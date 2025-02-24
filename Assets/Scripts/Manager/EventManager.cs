using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{

    public class DamgageEventArgs : EventArgs
    {
        public Damage Damage;
    }
    public event EventHandler<DamgageEventArgs> BeforeDamgageEvent;

    public event EventHandler<DamgageEventArgs> TakeDamgageEvent;
    
    public event EventHandler<DamgageEventArgs> DamgageDealtEvent;

    private void Start()
    {
        
    }

    public void DamageEvent(object sender, Damage damage)
    {
        if (damage.DamageTaker.IsDead) return;
        BeforeDamgageEvent?.Invoke(this, new DamgageEventArgs { Damage = damage});
        DamageHandler(ref damage);
        TakeDamgageEvent?.Invoke(this, new DamgageEventArgs { Damage = damage });
        DamgageDealtEvent?.Invoke(this, new DamgageEventArgs { Damage = damage });

    }

    

    public void DamageHandler(ref Damage damage)
    {
        switch (damage.DamageType)
        {
            case Damage.DamageTypeEnum.PhysicalDamage:
                damage.DamageAmount /= (1 + damage.DamageTaker.Armor / 100);
                break;
            case Damage.DamageTypeEnum.MagicDamage:
                damage.DamageAmount /= (1 + damage.DamageTaker.Shield / 100);
                break;
        }
        damage.DamageTaker.TakeDamage(damage);
    }

}
