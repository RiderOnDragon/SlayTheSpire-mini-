using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatusAbility : Ability
{
    private Status _status;

    public Status Status { get => _status; }

    public AddStatusAbility(Status status, Target target, int value, int numberTriggering) : base(target, value, numberTriggering)
    {
        _status = status;
    }

    protected override void UseChieldAbility(Unit target)
    {
        for (int i = 0; i < NumberTriggering; i++)
            target.AddStatus(_status);
    }
}
