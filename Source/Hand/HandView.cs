using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private float _angleRotationCardFan;
    [SerializeField] private float _offsetYCardFan;
    [SerializeField] private float _offsetXCardFan;

    [SerializeField] private float _adjacentCardsMoveDistance;

    private Hand _hand;

    private void Awake()
    {
        ShowingCard.ShowCard += OnShowCard;
        ShowingCard.HideCard += OnHideCard;
    }

    private void OnDestroy()
    {
        ShowingCard.ShowCard -= OnShowCard;
        ShowingCard.HideCard -= OnHideCard;
    }

    public void Init(Hand hand)
    {
        _hand = hand;
    }

    public void CreateCardFan()
    {
        for (int i = 0; i < _hand.CardsOnHand.Count; i++)
        {
            _hand.CardsOnHand[i].transform.localPosition = Vector2.zero;

            float alignIndex = i / (float)(_hand.CardsOnHand.Count - 1);

            if (alignIndex is float.NaN)
                alignIndex = 0.5f;

            float rotZ = Mathf.Lerp(_angleRotationCardFan * _hand.CardsOnHand.Count, -_angleRotationCardFan * _hand.CardsOnHand.Count, alignIndex);
            float offsetX = Mathf.Lerp(-_offsetXCardFan * _hand.CardsOnHand.Count, _offsetXCardFan * _hand.CardsOnHand.Count, alignIndex);
            float offsetY = -Mathf.Abs(Mathf.Lerp(_offsetYCardFan * _hand.CardsOnHand.Count, -_offsetYCardFan * _hand.CardsOnHand.Count, alignIndex));
            offsetY += -Mathf.Abs(_offsetYCardFan * Mathf.Sin(rotZ * Mathf.Deg2Rad));

            _hand.CardsOnHand[i].transform.rotation = Quaternion.Euler(0, 0, rotZ);
            _hand.CardsOnHand[i].transform.localPosition = new Vector2(offsetX, offsetY);
        }
    }

    private void OnShowCard(Card card)
    {
        MoveAdjacentCards(card, _adjacentCardsMoveDistance);
    }

    private void OnHideCard(Card card)
    {
        MoveAdjacentCards(card, -_adjacentCardsMoveDistance);
    }

    private void MoveAdjacentCards(Card midleCard, float distance)
    {
        int index = _hand.CardsOnHand.IndexOf(midleCard);

        int leftCardIndex = index > 0 ? (index - 1) : -1;
        int rightCardIndex = index < (_hand.CardsOnHand.Count - 1) ? (index + 1) : -1;

        if (leftCardIndex != -1)
        {
            float adjacentCardsCount = leftCardIndex + 1;
            for (int i = leftCardIndex; i >= 0; i--)
            {
                Card leftCard = _hand.CardsOnHand[i];
                float posX = leftCard.transform.localPosition.x - distance * ((i + 1) / adjacentCardsCount);
                leftCard.transform.localPosition = new Vector2(posX, leftCard.transform.localPosition.y);
            }

        }
        if (rightCardIndex != -1)
        {
            float adjacentCardsCount = rightCardIndex + 1;
            for (int i = rightCardIndex; i < _hand.CardsOnHand.Count; i++)
            {
                Card rightCard = _hand.CardsOnHand[i];
                float posX = rightCard.transform.localPosition.x + distance * (adjacentCardsCount / (i + 1));
                rightCard.transform.localPosition = new Vector2(posX, rightCard.transform.localPosition.y);
            }
        }
    }
}
