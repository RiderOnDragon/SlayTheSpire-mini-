using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStatus : TempStatus
{
    protected override int DecreasePerTick { get; } = 1;

    public PoisonStatus(Unit target, int value) : base(target, value)
    {
    }

    protected override void ApplyEffect(Unit taget)
    {
    }

    protected override void ApplyTickEffect(Unit target)
    {
        target.TakeDamage(Value, true);
    }
}
