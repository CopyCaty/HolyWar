using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public enum DamageTypeEnum
    {
        PhysicalDamage,
        MagicDamage,
        RealDamge
    }
    public enum DamageDealtTypeEnum
    {
        Melee,
        Projectile,
        Skill
    }

    public DamageTypeEnum DamageType;
    public DamageDealtTypeEnum DamageDealtType;
    public float DamageAmount;
    public AbstractUnit DamageSender;
    public AbstractUnit DamageTaker;
    public bool IsDogeAble;

    
}
