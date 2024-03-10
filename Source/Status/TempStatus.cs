using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TempStatus : Status
{
    protected virtual int DecreasePerTick { get; } = 1;

    public TempStatus(int value) : base(value)
    {
    }

    public IEnumerator Tick()
    {
        yield return ApplyTickEffect();
        Value -= DecreasePerTick;
    }

    protected virtual IEnumerator ApplyTickEffect() { yield return null; }
}
