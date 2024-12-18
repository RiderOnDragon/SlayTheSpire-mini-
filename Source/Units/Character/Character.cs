using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterView))]
public class Character : Unit
{
    [SerializeField] private CharacterView _view;

    private int _currentMana;

    public new TempCharacterData Data { get => (TempCharacterData)base.Data; }

    public int CurrentMana 
    {
        get { return _currentMana; }
        set {
            if (value < 0)
                throw new ArgumentException();

            if (value > Data.MaxMana)
                value = Data.MaxMana;

            _currentMana = value;
            _view.UpdateManaBar(_currentMana, Data.MaxMana);
        }
    }

    public static Character Singleton;

    protected override void ChildInit()
    {
        if (Singleton == null)
        {
            Singleton = this;

            Card.CardPlayed += OnCardPlayed;
            TurnSystem.NextTurn += OnNextTurn;
            RoomCompletor.CompletedRoom += SaveData;

            DontDestroyOnLoad(gameObject);
        }
        else if (Singleton != this)
        {
            Destroy(this);
        }

        _view.Init();

        CurrentHp = Data.CurrentHp;
        CurrentMana = Data.MaxMana;

        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Card.CardPlayed -= OnCardPlayed;
        TurnSystem.NextTurn -= OnNextTurn;
        RoomCompletor.CompletedRoom -= SaveData;
    }
    private void OnNextTurn()
    {
        StartCoroutine(NextTurn());
    }

    private void OnCardPlayed(Card card)
    {
        RemoveMana(card.Data.ManaCost);

        foreach (var ability in card.Data.Abilities)
        {
            switch (ability)
            {
                case DealDamageAbility:
                    Animation.Attack();
                    break;
                default:
                    Debug.LogError("The raw type of ability");
                    break;
            }
        }
    }

    private void RemoveMana(int count)
    {
        if (count < 0 || count > CurrentMana)
            throw new ArgumentException();

        CurrentMana -= count;
    }

    protected override void ChildOnNextTurn()
    {
        CurrentMana = Data.MaxMana;
    }

    private void SaveData()
    {
        Data.SaveData(CurrentHp);
    }
}