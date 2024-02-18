using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeckPanel : MonoBehaviour
{
    [SerializeField] private Transform _cardsContainer;

    private List<CardData> _cards = new List<CardData>();

    public void ShowPanel(List<CardData> cards)
    {
        foreach (CardData cardData in cards)
        {
            GameObject cell = new GameObject("DeckPanelCell", typeof(RectTransform));
            cell.transform.SetParent(_cardsContainer, false);

            var card = Instantiate(cardData.DeckPanelCardPrefab, cell.transform);
            card.Init(cardData);
        }

        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        foreach (Transform card in _cardsContainer)
            Destroy(card.gameObject);

        gameObject.SetActive(false);
    }
}
