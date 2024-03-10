using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStatus : TempStatus
{
    public PoisonStatus(int value) : base(value)
    {
    }

    protected override IEnumerator ApplyTickEffect()
    {
        yield return _target.TakeDamage(Value, true);
    }
}
