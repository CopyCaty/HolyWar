using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DamageAble
{
    public void TakeDamage(Damage damage);
    public void UpdateHealthBar();
    public void InitHealthBar();
}
