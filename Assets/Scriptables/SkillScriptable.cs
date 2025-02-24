using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSkill", menuName = "HolyWar/Skill")]
public class SkillScriptable : ScriptableObject
{
    //��������
    public string SkillName;
    //����ͼ��
    public Sprite SkillIcon;
    //��������
    public string SkillDescription;
    //������ȴʱ��
    public double[] SkillCoolDown;
    //������������
    public double[] SkillEnergyCost;
    public enum SkillTypeEnum
    {
        //�������ͷ�
        TARGET_ALLY,
        //������Ӣ���ͷ�
        TARGET_ALLY_SERVANT,
        //�Ե���Ӣ���ͷ�
        TARGET_ENEMY_SERVANT,
        //������Ӣ���ͷ�
        TARGET_ENEMY,
        //������Ŀ���ͷ�
        TARGET_ALL,
        //�Է����ͷ�
        DIRECTION,
        //�������ͷ�
        AREA,
        //ֱ���ͷ�
        DIRECT,
        //�����ͷ�
        PASSIVE
    }
    //��������
    public SkillTypeEnum SkillType;

}
