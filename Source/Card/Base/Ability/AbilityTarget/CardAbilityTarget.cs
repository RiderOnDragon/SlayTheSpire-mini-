using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[System.Serializable]
public abstract class CardAbilityTarget
{
    public readonly List<CardAbility> Abilities;

    public abstract string Description { get; }

    public CardAbilityTarget(List<CardAbility> abilities)
    {
        Abilities = abilities;
    }


    public abstract bool PrepareAbility(Unit target);

    public abstract void UseAbility();
}
