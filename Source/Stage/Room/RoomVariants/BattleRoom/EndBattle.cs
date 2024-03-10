using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattle : MonoBehaviour
{
    [SerializeField] private RevardPanel _rewardPanel;

    private void Awake()
    {
        EnemySquad.AllWavesDefeated += Win;
    }

    private void OnDestroy()
    {
        EnemySquad.AllWavesDefeated -= Win;
    }

    private void Win()
    {
        _rewardPanel.ShowReward();
    }
}
