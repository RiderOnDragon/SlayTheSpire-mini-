using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DeckView))]
public abstract class Deck : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform _cardsContainer;
    [SerializeField] private DeckPanel _deckPanel;
    
    [SerializeField] protected DeckView _view;

    protected List<CardData> _cards = new List<CardData>();

    public IList<CardData> Cards => _cards.AsReadOnly();

    public abstract List<Card> GetNextCards(int count);
    public abstract Card GetNextCard();

    public void Awake()
    {
        ChildInit();
    }

    protected virtual void ChildInit()
    {

    }

    protected void ShuffleDeck(List<CardData> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            var temp = deck[i];
            int index = UnityEngine.Random.Range(i, deck.Count);
            deck[i] = deck[index];
            deck[index] = temp;
        }
    }

    protected Card CreateCard(CardData cardData)
    {
        Card card = Instantiate(cardData.Prefab, _cardsContainer);
        card.Init(cardData);

        RemoveCardFromDeck(card.Data);

        return card;
    }

    public void AddCardToDeck(Card card)
    {
        _cards.Add(card.Data);
        _view.UpdateView(_cards.Count);
    }
    public void AddCardToDeck(CardData card)
    {
        _cards.Add(card);
        _view.UpdateView(_cards.Count);
    }

    public void AddCardsToDeck(List<Card> cards)
    {
        foreach(Card card in cards)
        {
            AddCardToDeck(card);
        }
    }
    public void AddCardsToDeck(List<CardData> cards)
    {
        foreach (CardData card in cards)
        {
            AddCardToDeck(card);
        }
    }

    public void RemoveCardFromDeck(Card card)
    {
        _cards.Remove(card.Data);
        _view.UpdateView(_cards.Count);
    }
    public void RemoveCardFromDeck(CardData card)
    {
        _cards.Remove(card);
        _view.UpdateView(_cards.Count);
    }

    public void RemoveCardsFromDeck(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            RemoveCardFromDeck(card);
        }
    }
    public void RemoveCardsFromDeck(List<CardData> cards)
    {
        foreach (CardData card in cards)
        {
            RemoveCardFromDeck(card);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        List<CardData> deck = new List<CardData>(_cards);

        ShuffleDeck(deck);

        _deckPanel.ShowPanel(deck);
    }
}
