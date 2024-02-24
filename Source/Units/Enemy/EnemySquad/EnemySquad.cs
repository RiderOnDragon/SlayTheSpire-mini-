using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySquadView))]
public class EnemySquad : MonoBehaviour
{
    [SerializeField] private EnemySquadView _view;

    [SerializeField] private Transform[] _unitPosition = new Transform[GameConst.MAX_ENEMY_IN_SQUAD];

    private List<EnemyWave> _wavesData = new List<EnemyWave>();

    private List<Enemy> _currentEnemies = new List<Enemy>();

    public IList<Enemy> CurrentEnemies { get => _currentEnemies.AsReadOnly(); }

    public static EnemySquad Singleton;

    public static event Action EnemyTurnFinished;
    public static event Action AllWavesDefeated;

    private const float TURN_DELAY = 0.2f;

    public void Init(List<EnemyWave> wavesData)
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(this);

        _wavesData.AddRange(wavesData);

        _view.Init(_wavesData.Count);

        InitNewWave();

        TurnSystem.PlayerTurnFinished += StartEnemyTurn;
    }

    private void OnDestroy()
    {
        TurnSystem.PlayerTurnFinished -= StartEnemyTurn;
    }

    public void InitNewWave()
    {
        _view.UpdateView();

        _currentEnemies.Clear();

        for (int i = 0; i < _wavesData[0].Enemys.Count; i++)
        {
            Enemy enemy = Instantiate(_wavesData[0].Enemys[i].Prefab, _unitPosition[i]);
            enemy.Init(_wavesData[0].Enemys[i]);
            _currentEnemies.Add(enemy);

            enemy.Death += OnEnemyDefeated;
        }

        _wavesData.Remove(_wavesData[0]);
    }

    private void OnEnemyDefeated(Unit enemy)
    {
        enemy.Death -= OnEnemyDefeated;

        _currentEnemies.Remove((Enemy)enemy);

        if (_currentEnemies.Count == 0)
        {
            if (_wavesData.Count == 0)
            {
                AllWavesDefeated?.Invoke();
                return;
            }

            InitNewWave();
        }
    }

    private void StartEnemyTurn()
    {
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(TURN_DELAY);

        for (int i = _currentEnemies.Count - 1; i >= 0; i--)
            yield return _currentEnemies[i].NextTurn();

        EnemyTurnFinished?.Invoke();
    }
}
