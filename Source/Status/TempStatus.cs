using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TempStatus : Status
{
    private Unit _target;

    protected abstract int DecreasePerTick { get; }

    public TempStatus(Unit target, int value) : base(target, value)
    {
        _target = target;
    }

    public void Tick()
    {
        ApplyTickEffect(_target);
        Value -= DecreasePerTick;
    }

    protected abstract void ApplyTickEffect(Unit target);
}
