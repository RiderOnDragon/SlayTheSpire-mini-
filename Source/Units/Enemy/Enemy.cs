using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(EnemyView), typeof(EnemyAnimation))]
public class Enemy : Unit
{
    [SerializeField] private EnemyAnimation _animation;
    [SerializeField] private EnemyView _view;
    private EnemyData Data { get => (EnemyData)_data; }

    private ActionPatterns _nextAction = null;
    private int _turn = 0;

    protected override UnitAnimation Animation { get => _animation; }

    protected override void ChildInit()
    {
        CurrentHp = _data.MaxHp;

        PrepareNextAction();

        TurnSystem.PlayerTurnFinished += RemoveShield;
        EnemySquad.EnemyTurnFinished += PrepareNextAction;
    }

    private void OnDestroy()
    {
        TurnSystem.PlayerTurnFinished -= RemoveShield;
        EnemySquad.EnemyTurnFinished -= PrepareNextAction;
    }

    public IEnumerator UsePattern()
    {
        switch (_nextAction.ActionType) 
        {
            case ActionPatterns.Type.WEAK_ATTACK:
                _animation.WeakAttack();
                break;
            case ActionPatterns.Type.SHIELD:
                _animation.AddShield();
                break;
        }

        _view.DisableActionView();

        yield return new WaitForSeconds(_animation.AnimationTime);
    }

    private void PrepareNextAction()
    {
        if (_turn > Data.ActionPatterns.Count - 1)
            _turn = 0;

        _nextAction = Data.ActionPatterns[_turn];

        var actionType = _nextAction.ActionType;
        int value = _nextAction.Value; //ѕозже будет мен€тьс€ в зависимости от наложенных ослаблений/усилений

        _view.ChangeActionView(actionType, value);

        _turn++;
    }

    private void RemoveAction()
    {
        _nextAction = null;
        _view.DisableActionView();
    }

    public void DealDamage()
    {
        Character.Singleton.TakeDamage(_nextAction.Value);

        RemoveAction();
    }

    public void ProtectYourself()
    {
        AddShield(_nextAction.Value);

        RemoveAction();
    }
}