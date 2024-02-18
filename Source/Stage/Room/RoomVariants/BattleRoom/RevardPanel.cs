using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RevardPanel : MonoBehaviour
{
    [SerializeField] private CardsDatabase _cardsDatabase;
    [SerializeField] private Transform _rewardCardsContainer;
    [SerializeField] private TextMeshProUGUI _selectRevardCardText;
    [SerializeField] private Button _giveRewardBtn;

    private List<DeckPanelCard> _rewardCards = new List<DeckPanelCard>();

    private CardData _rewardCardData = null;

    public void ShowReward()
    {
        var rewardCards = new List<CardData>();
        rewardCards.AddRange(_cardsDatabase.Cards[Character.Singleton.Data.Class]);

        for (int i = 0; i < Character.Singleton.Data.RewardCardsCount; i++)
        {
            int cardIndex = Random.Range(0, rewardCards.Count);
            var card = Instantiate(rewardCards[cardIndex].DeckPanelCardPrefab, _rewardCardsContainer);
            card.Init(rewardCards[cardIndex]);

            _rewardCards.Add(card);
            rewardCards.Remove(card.Data);

            if (rewardCards.Count == 0)
                break;
        }

        DeckPanelCard.SelectCard += ÑhooseRewardCard;

        gameObject.SetActive(true);
    }

    private void ÑhooseRewardCard(CardData cardData)
    {
        DeckPanelCard.SelectCard -= ÑhooseRewardCard;

        _rewardCardData = cardData;
        int cardIndex = _rewardCards.FindIndex(card => card.Data == _rewardCardData);
        _rewardCards.RemoveAt(cardIndex);

        foreach (var card in _rewardCards)
            Destroy(card.gameObject);

        _rewardCards.Clear();
        _selectRevardCardText.gameObject.SetActive(false);
        _giveRewardBtn.gameObject.SetActive(true);
    }

    public void GiveReward()
    {
        if (_rewardCardData == null)
            return;

        Character.Singleton.Data.AddCardToDeck(_rewardCardData);

        gameObject.SetActive(false);

        RoomCompletor.Singleton.CompleteRoom();
    }
}
