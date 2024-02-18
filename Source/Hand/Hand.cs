using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HandView))]
public class Hand : MonoBehaviour
{
    [SerializeField] private HandView _view;

    [SerializeField] private AvailableCardsDeck _availableCardsDeck;
    [SerializeField] private DiscardCardsDeck _discardCardsDeck;

    private List<Card> _cardsOnHand = new List<Card>();

    public IList<Card> CardsOnHand { get => _cardsOnHand.AsReadOnly(); }

    private void Awake()
    {
        TurnSystem.PlayerTurnFinished += OnPlayerTurnFinished;
        TurnSystem.NextTurn += OnNextTurn;
        Card.CardPlayed += OnCardPlayed;

    }

    private void OnDestroy()
    {
        TurnSystem.PlayerTurnFinished -= OnPlayerTurnFinished;
        TurnSystem.NextTurn -= OnNextTurn;
        Card.CardPlayed -= OnCardPlayed;
    }

    public void Init()
    {
        _view = GetComponent<HandView>();
        _view.Init(this);

        OnNextTurn();
    }

    private void OnPlayerTurnFinished()
    {
        foreach(var card in _cardsOnHand)
        {
            _discardCardsDeck.AddCardToDeck(card);
            Destroy(card.gameObject);
        }

        _cardsOnHand.Clear();
    }

    private void OnNextTurn()
    {
        int cardCount = Character.Singleton.Data.CountReceivedCards;
        AddCards(cardCount);
    }

    private void AddCards(int count)
    {
        List<Card> addedCards = new List<Card>();
        addedCards.AddRange(_availableCardsDeck.GetNextCards(count));

        if (_cardsOnHand.Count + addedCards.Count <= Character.Singleton.Data.MaxCardOnHand)
        {
            _cardsOnHand.AddRange(addedCards);
        }
        else
        {
            int addCardCount = Character.Singleton.Data.MaxCardOnHand - _cardsOnHand.Count;

            for (int i = 0; i < addCardCount; i++)
            {
                _cardsOnHand.Add(addedCards[0]);
                addedCards.Remove(addedCards[0]);
            }

            for (int i = 0; i < addedCards.Count; i++)
                Destroy(addedCards[i]);
        }

        _view.CreateCardFan();
    }

    private void OnCardPlayed(Card card)
    {
        _cardsOnHand.Remove(card);
        _view.CreateCardFan();
    }

}
