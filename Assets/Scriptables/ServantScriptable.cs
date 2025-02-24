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
    #region Ӣ����Ϣ
    //Ӣ����
    public string Name;
    //Ӣ��λ��
    public ServantClassEnum ServantClass;
    //Ӣ��ģ��

    //Ӣ��ͷ��

    //��������
    public enum AttactTypeEnum
    {
        Melee,
        Ranged
    }
    public AttactTypeEnum AttactType;
    #endregion
    #region ��������
    //�������ֵ
    public float Health;
    //�������ֵ
    public float Energy;
    //�����˺�
    public float WeaponDamage;
    //������
    public float Armor;
    //��������
    public float Shield;
    //�ƶ��ٶ�
    public float MoveSpeed;
    //��������
    public float AttackRange;
    //�����ٶ�
    public float AttackSpeed;
    //�����ָ�
    public float HealthRegen;
    //�����ظ�
    public float EnergyRegen;
    //����
    public SkillScriptable[] Skills;
    #endregion

    #region �ɳ�����
    public double HealthGrowth;
    //�������ֵ�ɳ�
    public double EnergyGrowth;
    //�����˺��ɳ�
    public double WeaponDamageGrowth;
    //�����׳ɳ�
    public double ArmorGrowth;
    //�������׳ɳ�
    public double ShieldGrowth;
    //�����ٶȳɳ�
    public double AttackSpeedGrowth;
    //�����ָ��ɳ�
    public double HealthRegenGrowth;
    //�����ظ��ɳ�
    public double EnergyRegenGrowth;
    #endregion
}
