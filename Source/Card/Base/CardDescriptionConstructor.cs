using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDescriptionConstructor
{
    //replace string
    private const string DAMAGE_VALUE = "DAMAGE_VALUE";
    private const string SHIELD_VALUE = "SHIELD_VALUE";
    private const string HEALING_VALUE = "HEALING_VALUE";
    private const string POISON_VALUE = "POISON_VALUE";
    private const string NUMBER_TRIGGERING = "xNUMBER_TRIGGERING";

    private Dictionary<Type, int> _replacePositions = new Dictionary<Type, int>();

    public string ConstructDescription(CardData cardData)
    {
        string description = cardData.Description;


        foreach (var ability in cardData.Abilities)
        {
            string replaceValue = string.Empty;

            switch (ability) 
            {
                case DealDamageAbility:
                    replaceValue = DAMAGE_VALUE;
                    break;
                case AddShieldAbility:
                    replaceValue = SHIELD_VALUE;
                    break;
                case HealingAbility:
                    replaceValue = HEALING_VALUE;
                    break;
                case AddStatusAbility addStatusAbility:
                    switch (addStatusAbility.Status)
                    {
                        case PoisonStatus:
                            replaceValue = POISON_VALUE;
                            break;
                    }
                    break;
                default:
                    throw new System.Exception("The raw type of ability");
            }

            ReplaceStringWithAbilityValue(ref description, replaceValue, ability);
        }

        return description;
    }

    private void ReplaceStringWithAbilityValue(ref string oldString, string replaceValue, Ability ability)
    {
        string newValue = ability.Value.ToString();

        if (ability.NumberTriggering > 1)
        {
            replaceValue += NUMBER_TRIGGERING;
            newValue += "x" + ability.NumberTriggering;
        }

        int replacePosition = oldString.IndexOf(replaceValue, 0);
        while (replacePosition > -1)
        {
            _replacePositions.Add(ability.GetType(), replacePosition);
            replacePosition = oldString.IndexOf(replaceValue, replacePosition + replaceValue.Length);
        }
        
        oldString = oldString.Replace(replaceValue, newValue);
    }
}
