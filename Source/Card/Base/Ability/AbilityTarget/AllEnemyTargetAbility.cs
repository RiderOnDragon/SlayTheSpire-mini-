using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AllEnemyTargetAbility : CardAbilityTarget
{
    private List<Enemy> _enemies = new List<Enemy>();

    public override string Description
    {
        get
        {
            string decription = string.Empty;

            foreach (var ability in Abilities)
                decription += ability.Description + " всем врагам.\n";
            
            return decription;
        }
    }


    public AllEnemyTargetAbility(List<CardAbility> abilities) : base(abilities)
    {
    }


    public override bool PrepareAbility(Unit target)
    {
        _enemies.Clear();
        _enemies.AddRange(EnemySquad.Singleton.CurrentEnemies);

        return true;
    }

    public override void UseAbility()
    {
        foreach (var target in _enemies)
        {
            foreach (var ability in Abilities)
                ability.UseAbility(target);
        }
    }
}
