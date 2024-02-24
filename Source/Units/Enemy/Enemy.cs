using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class Enemy : Unit
{
    [SerializeField] private EnemyView _view;
    private EnemyData Data { get => (EnemyData)_data; }

    private Ability _nextAction = null;
    private int _turn = 0;

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
        foreach (var ability in Data.ActionPatterns)
        {
            switch (ability.AbilityType)
            {
                case Ability.Type.DEAL_DAMAGE:
                    _animation.Attack();
                    break;
                case Ability.Type.ADD_SHIELD:
                    Debug.LogError("NotImplementedException");
                    break;
                case Ability.Type.HEALING:
                    Debug.LogError("NotImplementedException");
                    break;
                default:
                    throw new System.Exception("The raw type of ability");
            }
        }

        _view.DisableActionView();

        yield return new WaitForSeconds(_animation.AnimationTime);
    }

    private void PrepareNextAction()
    {
        if (_turn > Data.ActionPatterns.Count - 1)
            _turn = 0;

        _nextAction = Data.ActionPatterns[_turn];

        var actionType = _nextAction.AbilityType;
        int value = _nextAction.Value; //ѕозже будет мен€тьс€ в зависимости от наложенных ослаблений/усилений

        _view.ChangeActionView(actionType, value);

        _turn++;
    }
}