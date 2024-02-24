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

        EnemySquad.EnemyTurnFinished += PrepareNextAction;
    }

    private void OnDestroy()
    {
        EnemySquad.EnemyTurnFinished -= PrepareNextAction;
    }

    public IEnumerator NextTurn()
    {
        OnNextTurn();

        yield return UsePattern();
    }

    public IEnumerator UsePattern()
    {
        switch (_nextAction.AbilityType)
        {
            case Ability.Type.DEAL_DAMAGE:
                _animation.Attack();
                break;
            case Ability.Type.ADD_SHIELD:
                _animation.AddShield();
                break;
            case Ability.Type.HEALING:
                Debug.LogError("NotImplementedException");
                break;
            case Ability.Type.ADD_POISON_STATUS:
                Debug.LogError("NotImplementedException");
                break;
            default:
                throw new System.Exception("The raw type of ability");
        }

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
    private void RemoveAction()
    {
        _nextAction = null;
        _view.DisableActionView();
    }

    //Used in Animator
    public void DealDamage()
    {
        Character.Singleton.TakeDamage(_nextAction.Value);

        RemoveAction();
    }
    //Used in Animator
    public void ProtectYourself()
    {
        AddShield(_nextAction.Value);

        RemoveAction();
    }
}