using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardMover))]
public class Card : MonoBehaviour
{
    [SerializeField] private CardView _view;
    protected CardData _data;

    private bool _isPlayed = false;

    public CardView View { get => _view; }
    public CardData Data { get => _data; }

    public event Action FailedTryPlayCard;
    public static event Action<Card> CardPlayed;
    public static event Action<Card> CardDestroyed;

    public void Init(CardData data)
    {
        _data = data;
        _view.Init(_data);
    }

    private void OnDestroy()
    {
        if (_isPlayed == true)
            CardPlayed?.Invoke(this);
        else
            CardDestroyed?.Invoke(this);
    }

    public void PlayCard(Collider2D[] targets)
    {
        if (Character.Singleton.CurrentMana < _data.ManaCost)
        {
            FailedTryPlayCard?.Invoke();
            return;
        }

        Unit targetUnit = null;

        foreach (var target in targets)
        {
            if (target.TryGetComponent(out Unit unit) == true)
            {
                targetUnit = unit;
                break;
            }
        }

        if (TryPlayCard(targetUnit) == false)
        {
            FailedTryPlayCard?.Invoke();
            return;
        }

        UseAbility();

        _isPlayed = true;

        Destroy(gameObject);
    }

    protected bool TryPlayCard(Unit target)
    {
        foreach (var abilityTarget in Data.AbilitiesTargets)
        {
            if (abilityTarget.PrepareAbility(target) == false)
                return false;
        }

        return true;
    }
    protected void UseAbility()
    {
        foreach (var ability in Data.AbilitiesTargets)
            ability.UseAbility();
    }

    public void UpgradeCard()
    {
        Debug.Log("upgrade");
    }
}
