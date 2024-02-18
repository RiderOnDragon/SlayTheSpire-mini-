using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : UnitAnimation
{
    private const string WEAK_ATTACK = "WeakAttack";

    public void WeakAttack()
    {
        PlayAnimation(WEAK_ATTACK);
    }


}
