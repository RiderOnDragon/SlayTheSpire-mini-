using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckPanelCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CardView _view;
    [SerializeField] private Canvas _cardCanvas;

    [SerializeField] private float _showCardScale;

    private CardData _data;

    public CardData Data { get => _data; }

    public static event Action<CardData> ShowCardInfo;
    public static event Action<CardData> SelectCard;

    public void Init(CardData data)
    {
        _data = data;
        _view.Init(_data);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _cardCanvas.sortingOrder += 1;
        transform.localScale *= _showCardScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cardCanvas.sortingOrder -= 1;
        transform.localScale /= _showCardScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            SelectCard?.Invoke(_data);

        if (eventData.button == PointerEventData.InputButton.Right)
            ShowCardInfo?.Invoke(_data);
    }
}
