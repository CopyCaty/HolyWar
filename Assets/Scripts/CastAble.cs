using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CastAble
{
    public bool CheckCastAble(AbstractUnit unit);
    public void Cast(AbstractUnit unit);
}
