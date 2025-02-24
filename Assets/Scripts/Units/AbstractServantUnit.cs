using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractServantUnit : AbstractUnit
{
    public ServantScriptable ServantData;
    public SkillScriptable[] SkillData;
    public float Energy;
    public int Level;
    public float BasicAttackCoolDownTimer;
    public float BasicAttackCoolDown;
    public float WeaponBouns;
    //英雄身上的状态列表


    private void Awake()
    {
        UnitType = UnitTypeEnum.Servant;
        Level = 1;
        MaxHP = ServantData.Health;
        HP = ServantData.Health;
        Energy = ServantData.Energy;
        BasicAttackCoolDownTimer = 0;
    }

    public void Update()
    {
        if(BasicAttackCoolDownTimer > 0) BasicAttackCoolDownTimer -= Time.deltaTime;
    }

}
