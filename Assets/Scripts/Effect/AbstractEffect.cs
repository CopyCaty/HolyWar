using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEffect : ScriptableObject
{

    public virtual void OnEffect(AbstractUnit playerUnit, Vector3 pos) { }
    public virtual void OnEffect(AbstractUnit playerUnit, AbstractUnit targetUnit) { }
    
}
