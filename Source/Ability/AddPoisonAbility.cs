using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoisonAbility : Ability
{
    public override Type AbilityType { get; } = Type.ADD_POISON_STATUS;
    public override string Description { get; }

    public AddPoisonAbility(Target target, int value, int numberTriggering) : base(target, value, numberTriggering)
    {
    }


    protected override void UseChieldAbility(Unit target)
    {
        target.AddStatus(new PoisonStatus(target, Value));
        Debug.Log("poison");
    }
}
