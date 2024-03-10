using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class Enemy : Unit
{
    [SerializeField] private EnemyView _view;
    private new EnemyData Data { get => (EnemyData)base.Data; }

    private Ability _nextAction = null;
    private int _nextActionValue;
    private int _turn = 0;

    protected override void ChildInit()
    {
        PrepareNextAction();

        EnemySquad.EnemyTurnFinished += PrepareNextAction;
    }

    private void OnDestroy()
    {
        EnemySquad.EnemyTurnFinished -= PrepareNextAction;
    }

    public IEnumerator NextEnemyTurn()
    {
        yield return NextTurn();

        yield return UsePattern();
    }

    public IEnumerator UsePattern()
    {
        switch (_nextAction)
        {
            case DealDamageAbility:
                yield return Animation.Attack();
                break;
            case AddShieldAbility:
                yield return Animation.AddShield();
                break;
            default:
                throw new System.Exception("The raw type of ability");
        }
    }

    private void PrepareNextAction()
    {
        if (_turn > Data.ActionPatterns.Count - 1)
            _turn = 0;

        _nextAction = Data.ActionPatterns[_turn];

        _nextActionValue = _nextAction.Value;

        switch (_nextAction)
        {
            case DealDamageAbility:
                _nextActionValue = CalcDamage(Character.Singleton, _nextActionValue);
                break;
            case AddShieldAbility:
                _nextActionValue = CalcShield(_nextActionValue);
                break;
            default:
                throw new System.Exception("The raw type of ability");
        }

        _view.ChangeActionView(_nextAction, _nextActionValue);

        _turn++;
    }
    private void RemoveAction()
    {
        _nextAction = null;
        _view.DisableActionView();
    }

    #region USED IN ANIMATOR
    public void DealDamage()
    {
        Character.Singleton.TakeDamage(_nextActionValue);

        RemoveAction();
    }

    public void ProtectYourself()
    {
        AddShield(_nextActionValue);

        RemoveAction();
    }
    #endregion
}