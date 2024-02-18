using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInteface : MonoBehaviour
{
    [SerializeField] private Button _endTurnBtn;

    private void Awake()
    {
        TurnSystem.NextTurn += OnNextTurn;
        EnemySquad.AllWavesDefeated += HideInterface;
        Character.Death += HideInterface;
    }

    private void OnDestroy()
    {
        TurnSystem.NextTurn -= OnNextTurn;
        EnemySquad.AllWavesDefeated -= HideInterface;
        Character.Death -= HideInterface;
    }

    private void OnNextTurn()
    {
        _endTurnBtn.gameObject.SetActive(true);
    }

    private void HideInterface()
    {
        gameObject.SetActive(false);
    }
}