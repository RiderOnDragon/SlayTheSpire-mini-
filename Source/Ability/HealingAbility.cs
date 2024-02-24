using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAbility : Ability
{
    public override Type AbilityType { get; } = Type.HEALING;

    public override string Description
    {
        get
        {
            string description = $"Лечит {Value} здоровья";

            if (NumberTriggering > 1)
                description = description.Replace(" здоровья", $"x{NumberTriggering} здоровья");

            return description;
        }
    }

    public HealingAbility(Target target, int value, int numberTriggering) : base(target, value, numberTriggering)
    {
    }

    protected override void UseChieldAbility(Unit target)
    {
        target.Healing(Value);
    }
}
