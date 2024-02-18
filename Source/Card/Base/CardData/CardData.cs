using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class CardData : ScriptableObject
{
    [SerializeField] private Card _prefab;
    [SerializeField] private DeckPanelCard _deckPanelCardPrefab;

    [SerializeField] private List<Ability> _abilities = new List<Ability>();

    [Space(20)]

    [SerializeField, Min(0)] private int _manaCost;

    [Space(20)]

    [SerializeField] private string _name;

    [Space(20)]

    [SerializeField] private Sprite _background;
    [SerializeField] private Sprite _img;
    [SerializeField, HideInInspector] private Sprite _manaCostImg;

    private HashSet<CardAbilityTarget> _abilitiesTargets = new HashSet<CardAbilityTarget>();

    public Card Prefab { get => _prefab; }
    public DeckPanelCard DeckPanelCardPrefab { get => _deckPanelCardPrefab; }
    public abstract CharacterData.CharacterClass Affiliation { get; }
    public int ManaCost { get => _manaCost; }
    public HashSet<CardAbilityTarget> AbilitiesTargets { get => _abilitiesTargets; }
    public string Name { get => _name; }
    public string Description 
    {
        get
        {
            string description = "";

            foreach (var target in _abilitiesTargets)
                description += target.Description;

            return description;
        }
    }
    public Sprite Background { get => _background; }
    public Sprite Img { get => _img; }
    public Sprite ManaCostImg { get => _manaCostImg; }

    private void OnValidate()
    {
        foreach (var ability in _abilities)
            ability.Validate();

        HashSet<Ability.Target> targetTypeHash = new HashSet<Ability.Target>();
        List<Ability> dublicateTarget = _abilities.Where(ability => targetTypeHash.Add(ability.AbilityTarget) == false).ToList();

        //Один повтор необходим, чтобы можно было добавить тип и заменить его в случае повтора
        if (dublicateTarget.Count > 1)
        {
            for (int i = 1; i < dublicateTarget.Count; i++)
                _abilities.Remove(dublicateTarget[i]);

            throw new Exception("The target should not be repeated. Before adding, change the duplicate target");
        }

        //Так как один повтор необходим, но нужно чтобы все способноти были уникальны, пришлось создавать дополнительный HashSet и работать с ним
        targetTypeHash.Clear();
        HashSet<Ability> abilityHash = _abilities.Where(ability => targetTypeHash.Add(ability.AbilityTarget) == true).ToHashSet();

        foreach (var ability in abilityHash)
        {
            switch (ability.AbilityTarget)
            {
                case Ability.Target.SINGLE_ENEMY:
                    _abilitiesTargets.Add(new SingleEnemyTragetAbility(ability.Abilities.ToList()));
                    break;
                case Ability.Target.ALL_ENEMY:
                    _abilitiesTargets.Add(new AllEnemyTargetAbility(ability.Abilities.ToList()));
                    break;
                case Ability.Target.CHARACTER:
                    _abilitiesTargets.Add(new CharacterTargetAbility(ability.Abilities.ToList()));
                    break;
            }
        }
    }

    [System.Serializable]
    private class Ability
    {
        public enum Target
        {
            SINGLE_ENEMY,
            ALL_ENEMY,
            CHARACTER
        }

        [SerializeField] private Target _target;
        [SerializeField] private List<Action> _actions = new List<Action>();

        private List<CardAbility> _abilities = new List<CardAbility>();

        public Target AbilityTarget { get => _target; }
        public IList<CardAbility> Abilities { get => _abilities.AsReadOnly(); }

        public void Validate()
        {
            _abilities.Clear();

            HashSet<CardAbility.Type> actionTypesHash = new HashSet<CardAbility.Type>();
            List<Action> dublicateAction = _actions.Where(action => actionTypesHash.Add(action.ActionType) == false).ToList();

            if (dublicateAction.Count > 1)
            {
                for (int i = 1; i < dublicateAction.Count; i++)
                    _actions.Remove(dublicateAction[i]);

                throw new Exception("The types of action should not be repeated. Before adding, change the duplicate type");
            }

            actionTypesHash.Clear();
            HashSet<Action> actionHash = _actions.Where(action => actionTypesHash.Add(action.ActionType) == true).ToHashSet();

            foreach (var action in actionHash)
            {
                switch (action.ActionType)
                {
                    case CardAbility.Type.DEAL_DAMAGE:
                        _abilities.Add(new DealDamageAbility(action.Values, action.NumberTriggering));
                        break;
                    case CardAbility.Type.HEALING:
                        _abilities.Add(new HealingAbility(action.Values, action.NumberTriggering));
                        break;
                    case CardAbility.Type.ADD_SHIELD:
                        _abilities.Add(new AddShieldAbility(action.Values, action.NumberTriggering));
                        break;
                }
            }
        }


        [System.Serializable]
        public struct Action
        {
            [SerializeField] CardAbility.Type _type;
            [Tooltip("The array is needed in the case of a multi-action")]
            [SerializeField, Min(1)] private int _value;
            [SerializeField, Min(1)] private int _numberTriggering;

            public CardAbility.Type ActionType { get => _type; }
            public int Values { get => _value; }
            public int NumberTriggering { get => _numberTriggering; }
        }
    }
}