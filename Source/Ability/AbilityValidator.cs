using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AbilityValidator 
{
    public static List<Ability> Validate(List<AbilityStruct> abilitiesStruct)
    {
        HashSet<Ability> abilities = new HashSet<Ability>();

        var abilityHash = abilitiesStruct.Distinct().ToList();

        foreach (var ability in abilityHash)
        {
            Ability.Target target = ability.Target;
            int value = ability.Value;
            int numberTriggering = ability.NumberTriggering;

            switch (ability.AbilityType)
            {
                case AbilityType.DEAL_DAMAGE:
                    abilities.Add(new DealDamageAbility(target, value, numberTriggering));
                    break;
                case AbilityType.ADD_SHIELD:
                    abilities.Add(new AddShieldAbility(target, value, numberTriggering));
                    break;
                case AbilityType.HEALING:
                    abilities.Add(new HealingAbility(target, value, numberTriggering));
                    break;
                case AbilityType.ADD_POISON_STATUS:
                    abilities.Add(new AddStatusAbility(new PoisonStatus(value), target, value, numberTriggering));
                    break;
                default:
                    throw new System.Exception("The raw type of ability");
            }
        }

        return abilities.ToList();
    }

    [System.Serializable]
    public struct AbilityStruct
    {
        [SerializeField] private Ability.Target _target;
        [SerializeField] private AbilityType _type;
        [SerializeField] private int _value;
        [Tooltip("The array is needed in the case of a multi-action")]
        [SerializeField] private int _numberTriggering;

        public Ability.Target Target { get => _target; }
        public AbilityType AbilityType { get => _type; }
        public int Value { get => _value; }
        public int NumberTriggering { get => _numberTriggering; }
    }
}
