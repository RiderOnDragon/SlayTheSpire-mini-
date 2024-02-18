using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySquadView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveCountText;

    private int _wavesCount;
    private int _currentWave = 0;

    public void Init(int wavesCount)
    {
        _wavesCount = wavesCount;
    }

    public void UpdateView()
    {
        _currentWave++;
        _waveCountText.text = $"{_currentWave} / {_wavesCount}";
    }
}
