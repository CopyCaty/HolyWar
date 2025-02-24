using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewServant", menuName = "HolyWar/Servant")]
public class ServantScriptable : ScriptableObject
{
    public enum ServantClassEnum
    {
        SABER,
        LANCER,
        ARCHER,
        RIDER,
        CASTER,
        ASSASSIN,
        BERSERKER
    }
    #region 英雄信息
    //英雄名
    public string Name;
    //英雄位阶
    public ServantClassEnum ServantClass;
    //英雄模型

    //英雄头像

    //攻击类型
    public enum AttactTypeEnum
    {
        Melee,
        Ranged
    }
    public AttactTypeEnum AttactType;
    #endregion
    #region 基础属性
    //最大生命值
    public float Health;
    //最大能量值
    public float Energy;
    //武器伤害
    public float WeaponDamage;
    //物理护甲
    public float Armor;
    //法术护甲
    public float Shield;
    //移动速度
    public float MoveSpeed;
    //攻击距离
    public float AttackRange;
    //攻击速度
    public float AttackSpeed;
    //生命恢复
    public float HealthRegen;
    //能量回复
    public float EnergyRegen;
    //技能
    public SkillScriptable[] Skills;
    #endregion

    #region 成长属性
    public double HealthGrowth;
    //最大能量值成长
    public double EnergyGrowth;
    //武器伤害成长
    public double WeaponDamageGrowth;
    //物理护甲成长
    public double ArmorGrowth;
    //法术护甲成长
    public double ShieldGrowth;
    //攻击速度成长
    public double AttackSpeedGrowth;
    //生命恢复成长
    public double HealthRegenGrowth;
    //能量回复成长
    public double EnergyRegenGrowth;
    #endregion
}
