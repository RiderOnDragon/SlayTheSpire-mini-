using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageAbility : Ability
{    
    public DealDamageAbility(Target target, int value, int numberTriggering) : base(target, value, numberTriggering)
    {
    }

    protected override void UseChieldAbility(Unit target)
    {
        target.TakeDamage(Value);
    }
}
