using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSkill", menuName = "HolyWar/Skill")]
public class SkillScriptable : ScriptableObject
{
    //技能名称
    public string SkillName;
    //技能图标
    public Sprite SkillIcon;
    //技能描述
    public string SkillDescription;
    //技能冷却时间
    public double[] SkillCoolDown;
    //技能能量消耗
    public double[] SkillEnergyCost;
    public enum SkillTypeEnum
    {
        //对盟友释放
        TARGET_ALLY,
        //对盟友英雄释放
        TARGET_ALLY_SERVANT,
        //对敌人英雄释放
        TARGET_ENEMY_SERVANT,
        //对盟友英雄释放
        TARGET_ENEMY,
        //对任意目标释放
        TARGET_ALL,
        //对方向释放
        DIRECTION,
        //对区域释放
        AREA,
        //直接释放
        DIRECT,
        //被动释放
        PASSIVE
    }
    //技能类型
    public SkillTypeEnum SkillType;

}
