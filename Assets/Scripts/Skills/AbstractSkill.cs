using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractSkill : CastAble
{
    public SkillScriptable skillScriptable;

    public void Cast()
    {
        throw new System.NotImplementedException();
    }
}
