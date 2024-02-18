using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CampPanel : MonoBehaviour
{
    [SerializeField] private DeckPanel _deckPanel;

    private Character _character;

    public event Action FinishRoom;

    public void Init()
    {
        _character = Character.Singleton;
        DeckPanelCard.SelectCard += UpgradeCard;
    }

    private void OnDestroy()
    {
        DeckPanelCard.SelectCard -= UpgradeCard;
    }

    public void Healing()
    {
        _character.Healing(_character.Data.RestoringHp);

        ClosePanel();
    }

    public void ShowDeckPanel()
    {
        _deckPanel.ShowPanel(_character.Data.Deck.ToList());
    }

    private void UpgradeCard(CardData card)
    {
        Debug.Log("UpgradeCard");
        ClosePanel();
    }

    private void ClosePanel()
    {
        _deckPanel.ClosePanel();
        gameObject.SetActive(false);

        RoomCompletor.Singleton.CompleteRoom();
    }
}
