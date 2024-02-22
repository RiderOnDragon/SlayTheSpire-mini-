using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class CardData : ScriptableObject
{
    [SerializeField] private Card _prefab;
    [SerializeField] private DeckPanelCard _deckPanelCardPrefab;

    [SerializeField] private List<AbilityValidator.AbilityStruct> _cardAbilities = new List<AbilityValidator.AbilityStruct>();

    [Space(20)]

    [SerializeField, Min(0)] private int _manaCost;

    [Space(20)]

    [SerializeField] private string _name;

    [Space(20)]

    [SerializeField] private Sprite _background;
    [SerializeField] private Sprite _img;
    [SerializeField, HideInInspector] private Sprite _manaCostImg;

    private List<Ability> _abilities = new List<Ability>();

    public abstract CharacterData.CharacterClass Affiliation { get; }

    public Card Prefab { get => _prefab; }
    public DeckPanelCard DeckPanelCardPrefab { get => _deckPanelCardPrefab; }
    public IList<Ability> Abilities { get => _abilities.AsReadOnly(); }
    public int ManaCost { get => _manaCost; }
    public string Name { get => _name; }
    public string Description 
    {
        get
        {
            string description = string.Empty;

            return description;
        }
    }
    public Sprite Background { get => _background; }
    public Sprite Img { get => _img; }
    public Sprite ManaCostImg { get => _manaCostImg; }

    private void OnValidate()
    {
        _abilities = AbilityValidator.Validate(_cardAbilities);
    }
}