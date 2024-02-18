using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AvailableCardsDeck : Deck
{
    [SerializeField] private DiscardCardsDeck _discardCardsDeck;

    public void Init(List<CardData> deck)
    {
        AddCardsToDeck(deck);
        ShuffleDeck(_cards);
    }

    public override Card GetNextCard()
    {
        if (Cards.Count == 0)
            return null;

        Card card = CreateCard(_cards[0]);

        if (_cards.Count == 0)
        {
            if (_discardCardsDeck.Cards.Count > 0)
            {
                AddCardsToDeck(_discardCardsDeck.GetWholeDeck());
                ShuffleDeck(_cards);
            }
        }

        return card;
    }

    public override List<Card> GetNextCards(int count)
    {
        List<Card> cards = new List<Card>();

        if (_cards.Count < count)
        {
            int availableCardscount = _cards.Count;

            count -= availableCardscount;
            for (int i = 0; i < availableCardscount; i++)
                cards.Add(CreateCard(_cards[0]));

            AddCardsToDeck(_discardCardsDeck.GetWholeDeck());
            ShuffleDeck(_cards);

            if (_cards.Count < count)
            {
                availableCardscount = _cards.Count;
                for (int i = 0; i < availableCardscount; i++)
                    cards.Add(CreateCard(_cards[0]));

                return cards;
            }
        }

        for (int i = 0; i < count; i++)
            cards.Add(CreateCard(_cards[0]));

        return cards;
    }
}
