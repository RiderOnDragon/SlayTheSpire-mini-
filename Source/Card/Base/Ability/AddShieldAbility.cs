using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShieldAbility : CardAbility
{
    public override Type AbilityType { get; } = Type.ADD_SHIELD;
    public override string Description
    {
        get
        {
            string description = $"��� {_value} �����";

            if (_numberTriggering > 1)
                description = description.Replace(" �����", $"x{_numberTriggering} �����");

            return description;
        }
    }

    private Character _character;

    public AddShieldAbility(int value, int numberTriggering) : base(value, numberTriggering)
    {
    }

    protected override void UseChieldAbility(Unit target)
    {
        target.AddShield(_value);
    }
}
