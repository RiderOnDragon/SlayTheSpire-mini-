using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAbility : Ability
{
    public HealingAbility(Target target, int value, int numberTriggering) : base(target, value, numberTriggering)
    {
    }

    protected override void UseChieldAbility(Unit target)
    {
        target.Healing(Value);
    }
}
