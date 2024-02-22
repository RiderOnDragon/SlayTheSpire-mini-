using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageAbility : Ability
{
    public override Type AbilityType { get; } = Type.DEAL_DAMAGE;

    public override string Description
    {
        get
        {
            string description = $"Наносит {_value} урона";

            if (_numberTriggering > 1)
                description = description.Replace(" урона", $"x{_numberTriggering} урона");

            return description;
        }
    }
    
    public DealDamageAbility(Target target, int value, int numberTriggering) : base(target, value, numberTriggering)
    {
    }

    protected override void UseChieldAbility(Unit target)
    {
        target.TakeDamage(_value);
    }
}
