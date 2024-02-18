using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterTargetAbility : CardAbilityTarget
{
    private Character _character;

    public override string Description
    {
        get
        {
            string decription = string.Empty;

            foreach (var ability in Abilities)
                decription += ability.Description + " персонажу.\n";

            return decription;
        }
    }

    public CharacterTargetAbility(List<CardAbility> abilities) : base(abilities)
    {
    }

    public override bool PrepareAbility(Unit target)
    {
        _character = Character.Singleton;
        return true;
    }

    public override void UseAbility()
    {
        foreach (var ability in Abilities)
            ability.UseAbility(_character);
    }
}
