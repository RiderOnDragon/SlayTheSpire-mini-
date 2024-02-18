using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SingleEnemyTragetAbility : CardAbilityTarget
{
    private Enemy _enemy;

    public override string Description
    {
        get
        {
            string decription = string.Empty;

            foreach (var ability in Abilities)
                decription += ability.Description + " врагу.\n";

            return decription;
        }
    }

    public SingleEnemyTragetAbility(List<CardAbility> abilities) : base(abilities)
    {
    }

    public override bool PrepareAbility(Unit target)
    {
        if (target == null)
            return false;

        if (target is Enemy enemy)
        {
            _enemy = enemy;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void UseAbility()
    {
        foreach (var ability in Abilities)
            ability.UseAbility(_enemy);
    }
}
