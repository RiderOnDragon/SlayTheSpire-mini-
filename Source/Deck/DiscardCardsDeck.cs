using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCardsDeck : Deck
{
    protected override void ChildInit()
    {
        Card.CardPlayed += AddCardToDeck;
        Card.CardDestroyed += AddCardToDeck;
    }

    private void OnDestroy()
    {
        Card.CardPlayed -= AddCardToDeck;
        Card.CardDestroyed -= AddCardToDeck;
    }

    public override Card GetNextCard()
    {
        if (Cards.Count == 0)
            return null;

        Card card = CreateCard(_cards[0]);

        return card;
    }

    public override List<Card> GetNextCards(int count)
    {
        if (_cards.Count == 0)
            return null;

        if (count > _cards.Count)
            count = _cards.Count;
        
        List<Card> cards = new List<Card>();

        for (int i = 0; i < count; i++)
        {
            Card card = GetNextCard();
            cards.Add(card);
        }

        return cards;
    }

    public List<CardData> GetWholeDeck()
    {
        List<CardData> cards = new List<CardData>(_cards);
        _cards.Clear();
        _view.UpdateView(_cards.Count);

        return cards;
    }
}
