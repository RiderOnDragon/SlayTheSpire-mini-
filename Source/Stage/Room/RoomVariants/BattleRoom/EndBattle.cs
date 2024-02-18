using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattle : MonoBehaviour
{
    [SerializeField] private RevardPanel _rewardPanel;
    [SerializeField] private GameObject _losePanel;

    private void Awake()
    {
        EnemySquad.AllWavesDefeated += Win;
        Character.Death += Lose;
    }

    private void OnDestroy()
    {
        EnemySquad.AllWavesDefeated -= Win;
        Character.Death -= Lose;
    }

    private void Win()
    {
        _rewardPanel.ShowReward();
    }

    private void Lose()
    {
        _losePanel.SetActive(true);
    }
}
