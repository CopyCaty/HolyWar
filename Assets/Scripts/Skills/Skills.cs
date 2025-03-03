using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractSkill : CastAble
{
    public SkillScriptable skillScriptable;

    public bool CheckCastAble(AbstractUnit unit)
    {
        return true;
    }

    public void Cast(AbstractUnit unit)
    {
        throw new System.NotImplementedException();
    }
}
