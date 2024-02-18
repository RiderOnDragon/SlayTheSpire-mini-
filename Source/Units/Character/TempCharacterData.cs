using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TempCharacterData : CharacterData
{
    private int _currentHP;

    public new int RestoringHp
    {
        get { return _restoringHp; }
        set
        {
            if (value < 0)
                throw new System.ArgumentException();

            _restoringHp = value;
        }
    }
    public new int MaxHp 
    {
        get { return _maxHp; }
        set {
            if (value < 0)
                throw new System.ArgumentException();

            _maxHp = value;
        }
    }
    public int CurrentHp
    {
        get { return _currentHP; }
        set {
            if (value < 0)
                throw new System.ArgumentException();

            if (value > MaxHp)
                value = MaxHp;

            _currentHP = value;
        }
    }
    public new int MaxMana
    {
        get { return _maxMana; }
        set
        {
            if (value < 0)
                throw new System.ArgumentException();

            _maxMana = value;
        }
    }
    public new int MaxCardOnHand
    {
        get { return _maxCardOnHand; }
        set
        {
            if (value < 0)
                throw new System.ArgumentException();

            _maxCardOnHand = value;
        }
    }
    public new int CountReceivedCards
    {
        get { return _countReceivedCards; }
        set
        {
            if (value < 0)
                throw new System.ArgumentException();

            _countReceivedCards = value;
        }
    }
    public new int RewardCardsCount
    {
        get { return _rewardCardsCount; }
        set
        {
            if (value < 0)
                throw new System.ArgumentException();

            _rewardCardsCount = value;
        }
    }
    public IList<CardData> Deck { get => _startDeck.AsReadOnly(); }

    public void Init(CharacterData characterData)
    {
        _prefab = characterData.Prefab;
        _class = characterData.Class;
        _maxHp = characterData.MaxHp;
        _currentHP = characterData.MaxHp;
        _restoringHp = characterData.RestoringHp;
        _maxMana = characterData.MaxMana;
        _maxCardOnHand = characterData.MaxCardOnHand;
        _countReceivedCards = characterData.CountReceivedCards;
        _rewardCardsCount = characterData.RewardCardsCount;
        _startDeck = characterData.StartDeck.ToList();
    }

    public void AddCardToDeck(CardData card)
    {
        _startDeck.Add(card);
    }
}
