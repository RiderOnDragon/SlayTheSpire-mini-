using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CardAbility
{
    public enum Type
    {
        DEAL_DAMAGE,
        HEALING,
        ADD_SHIELD
    }

    protected readonly int _value;
    protected readonly int _numberTriggering;

    public abstract Type AbilityType { get; }
    public abstract string Description { get; }

    public CardAbility(int value, int numberTriggering)
    {
        _value = value;
        _numberTriggering = numberTriggering;
    }

    public void UseAbility(Unit target)
    {
        if (target == null)
            throw new System.ArgumentNullException();

        for (int i = 0; i < _numberTriggering; i++)
            UseChieldAbility(target);
    }

    protected abstract void UseChieldAbility(Unit target);
}
