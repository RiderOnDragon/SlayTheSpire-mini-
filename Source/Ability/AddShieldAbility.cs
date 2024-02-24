using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShieldAbility : Ability
{
    public override Type AbilityType { get; } = Type.ADD_SHIELD;

    public override string Description
    {
        get
        {
            string description = $"Даёт {Value} брони";

            if (NumberTriggering > 1)
                description = description.Replace(" брони", $"x{NumberTriggering} брони");

            return description;
        }
    }


    private Character _character;

    public AddShieldAbility(Target target, int value, int numberTriggering) : base(target, value, numberTriggering)
    {
    }

    protected override void UseChieldAbility(Unit target)
    {
        target.AddShield(Value);
    }
}
