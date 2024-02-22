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

    public void PlayCard(Collider2D target)
    {
        if (Character.Singleton.CurrentMana < _data.ManaCost)
        {
            FailedTryPlayCard?.Invoke();
            return;
        }

        Unit targetUnit = null;

        if (target != null && target.TryGetComponent(out Unit unit) == true)
            targetUnit = unit;

        foreach (var ability in Data.Abilities)
        {
            if (ability.CheckTarget(ref targetUnit) == false)
            {
                FailedTryPlayCard?.Invoke();
                return;
            }
        }

        foreach (var ability in Data.Abilities)
            ability.UseAbility(targetUnit);

        _isPlayed = true;

        Destroy(gameObject);
    }

    public void UpgradeCard()
    {
        Debug.Log("upgrade");
    }
}
