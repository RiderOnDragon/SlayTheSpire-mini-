using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static event Action PlayerTurnFinished;
    public static event Action NextTurn;

    private void Awake()
    {
        EnemySquad.EnemyTurnFinished += OnEnemyTurnFinished;
    }

    private void OnDestroy()
    {
        EnemySquad.EnemyTurnFinished -= OnEnemyTurnFinished;
    }

    public void FinishTurn()
    {
        PlayerTurnFinished?.Invoke();
    }

    private void OnEnemyTurnFinished()
    {
        NextTurn?.Invoke();
    }
}
