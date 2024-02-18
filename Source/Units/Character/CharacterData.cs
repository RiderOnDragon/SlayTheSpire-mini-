using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Create CharacterData")]
public class CharacterData : UnitData
{
    public enum CharacterClass
    {
        KNIGHT
    }

    [SerializeField] protected Character _prefab;
    [SerializeField] protected CharacterClass _class;
    [Tooltip("restoring health when visiting the camp")]
    [SerializeField] protected int _restoringHp;
    [SerializeField] protected int _maxMana;
    [SerializeField] protected int _maxCardOnHand;
    [SerializeField] protected int _countReceivedCards;
    [SerializeField] protected int _rewardCardsCount;
    [SerializeField] protected List<CardData> _startDeck;

    public Character Prefab { get => _prefab; }
    public int RestoringHp { get => _restoringHp; }
    public int MaxMana { get => _maxMana; }
    public int MaxCardOnHand { get => _maxCardOnHand; }
    public int CountReceivedCards { get => _countReceivedCards; }
    public int RewardCardsCount { get => _rewardCardsCount; }
    public IList<CardData> StartDeck { get => _startDeck.AsReadOnly(); }
    public CharacterClass Class { get => _class; }
}
