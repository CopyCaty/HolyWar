using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{

    public DamageEvent damageEvent;

    private void Awake()
    {
        damageEvent = new DamageEvent();
    }
    private void OnEnable()
    {
        damageEvent.OnTakeDamageTrigger += DamageHandler;
    }

    private void OnDisable()
    {
        damageEvent.OnTakeDamageTrigger -= DamageHandler;
    }



    public void DamageHandler(Damage damage)
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
