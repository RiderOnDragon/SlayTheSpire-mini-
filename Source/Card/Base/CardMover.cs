using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMover : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Card _card;

    private Camera _mainCamera;
    private Vector2 _offsetCursor;

    private Vector2 _oldPosition;

    private void Awake()
    {
        _card = GetComponent<Card>();

        _card.FailedTryPlayCard += ReturnCard;
        TurnSystem.PlayerTurnFinished += Disable;
        TurnSystem.NextTurn += Enable;
    }

    private void OnDestroy()
    {
        _card.FailedTryPlayCard -= ReturnCard;
        TurnSystem.PlayerTurnFinished -= Disable;
        TurnSystem.NextTurn -= Enable;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _mainCamera = Camera.main;

        _offsetCursor = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _oldPosition = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = (Vector3)_offsetCursor + _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        _card.PlayCard(colliders);
    }

    protected void ReturnCard()
    {
        transform.localPosition = _oldPosition;
    }

    private void Disable()
    {
        enabled = false;
    }

    private void Enable()
    {
        enabled = true;
    }
}
