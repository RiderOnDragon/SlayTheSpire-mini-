using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInfoPanel : MonoBehaviour
{
    [SerializeField] private CardView _cardView;
    [SerializeField] private GameObject _cardInfoPanel;

    [SerializeField] private Toggle _showUpgradeToggle;

    private CardData _cardData;

    private void Awake()
    {
        DeckPanelCard.ShowCardInfo += Init;
    }

    private void OnDestroy()
    {
        DeckPanelCard.ShowCardInfo -= Init;
    }

    public void Init(CardData cardData)
    {
        _cardData = cardData;

        _cardView.Init(_cardData);

        _cardInfoPanel.SetActive(true);
    }

    public void OnToggleClick()
    {
        if (_showUpgradeToggle.isOn == true)
            _cardView.Init(_cardData); //Позже добавить улучшение для карт и исправить, чтобы отображались улучшения
        else
            _cardView.Init(_cardData);
    }
}
