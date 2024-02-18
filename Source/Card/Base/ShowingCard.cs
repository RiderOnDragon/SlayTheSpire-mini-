using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowingCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Card _card;

    [SerializeField] private Canvas _cardCanvas;

    [SerializeField] private float _showCardScale;
    [SerializeField] private Vector2 _showCardPosition;

    private Vector2 _oldPosition;
    private Vector3 _oldRotation;

    public static event Action<Card> ShowCard;
    public static event Action<Card> HideCard;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _cardCanvas.sortingOrder += 1;

        _oldPosition = transform.localPosition;
        _oldRotation = transform.localRotation.eulerAngles;

        transform.localPosition = new Vector2(transform.localPosition.x, 0);

        transform.localPosition += (Vector3)_showCardPosition;
        transform.localRotation = Quaternion.Euler(Vector2.zero);
        transform.localScale *= _showCardScale;
        

        ShowCard?.Invoke(_card);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cardCanvas.sortingOrder -= 1;

        transform.localPosition = _oldPosition;
        transform.localRotation = Quaternion.Euler(_oldRotation);

        transform.localScale /= _showCardScale;

        HideCard?.Invoke(_card);
    }
}
