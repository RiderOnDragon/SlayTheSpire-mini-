using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimation), typeof(CharacterView))]
public class Character : Unit
{
    [SerializeField] private CharacterAnimation _animation;
    [SerializeField] private CharacterView _view;

    private int _currentMana;

    public TempCharacterData Data { get => (TempCharacterData)_data; }

    protected override UnitAnimation Animation { get => _animation; }

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

    public new static event Action Death;

    protected override void ChildInit()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(this);

        _view.Init();

        CurrentHp = Data.CurrentHp;
        CurrentMana = Data.MaxMana;

        Card.CardPlayed += OnCardPlayed;
        TurnSystem.NextTurn += OnNextTurn;
        RoomCompletor.CompletedRoom += UpdateData;
    }

    private void OnDestroy()
    {
        Card.CardPlayed -= OnCardPlayed;
        TurnSystem.NextTurn -= OnNextTurn;
        RoomCompletor.CompletedRoom -= UpdateData;
    }

    private void OnCardPlayed(Card card)
    {
        RemoveMana(card.Data.ManaCost);

        foreach (var target in card.Data.AbilitiesTargets)
        {
            foreach (var ability in target.Abilities)
            {
                switch (ability.AbilityType)
                {
                    case CardAbility.Type.DEAL_DAMAGE:
                        _animation.Attack();
                        break;
                }
            }
        }
        
    }

    private void RemoveMana(int count)
    {
        if (count < 0 || count > CurrentMana)
            throw new ArgumentException();

        CurrentMana -= count;
    }

    private void OnNextTurn()
    {
        RemoveShield();

        CurrentMana = Data.MaxMana;
    }

    private void UpdateData()
    {
        Data.CurrentHp = CurrentHp;
    }
}