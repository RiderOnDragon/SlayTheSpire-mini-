using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShieldAbility : Ability
{
    public AddShieldAbility(Target target, int value, int numberTriggering) : base(target, value, numberTriggering)
    {
    }

    protected override void UseChieldAbility(Unit target)
    {
        target.AddShield(Value);
    }
}
