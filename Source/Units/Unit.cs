using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(UnitAnimation))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] private HpBar _hpBar;
    [SerializeField] private Collider2D _mainCollider;

    private UnitAnimation _animation;
    private UnitData _data;
    private UnitStatus _currentStatus;

    private List<Status> _statuses = new List<Status>();

    private int _currentHp;
    private int _shield;

    protected UnitAnimation Animation { get => _animation; }
    protected UnitData Data { get => _data; }

    protected int CurrentHp 
    {
        get { return _currentHp; }
        set {
            if (value <= 0)
            {
                _mainCollider.enabled = false;
                Animation.Death();
            }

            if (value > _data.MaxHp)
                value = _data.MaxHp;

            _currentHp = value;
            _hpBar.UpdataHpBar(_currentHp, _data.MaxHp);
        }
    }
    protected int Shield 
    {
        get { return _shield; } 
        set
        {
            _shield = value;
            _hpBar.UpdataShieldBar(_shield);
        }
    }

    public UnitStatus CurrentStatus { get => _currentStatus; }

    public event Action<Unit> Death;

    public void Init(UnitData data)
    {
        _animation = GetComponent<UnitAnimation>();

        _data = data;
        _currentStatus = new UnitStatus();
        CurrentHp = _data.MaxHp;
        Animation.Init();

        ChildInit();
    }

    private void OnDestroy()
    {
        foreach (var status in _statuses)
            status.StatusFinished -= RemoveStatus;
    }

    protected abstract void ChildInit();
    protected virtual void ChildOnNextTurn() { }

    protected IEnumerator NextTurn()
    {
        yield return TickTempStatuses();
        
        RemoveShield();

        ChildOnNextTurn();
    }

    public Coroutine TakeDamage(int damage, bool ignoreShield = false)
    {
        if (damage < 0)
            throw new ArgumentException();

        if (ignoreShield == false && Shield > 0)
        {
            if (damage > Shield)
            {
                damage -= Shield;
                RemoveShield();
            }
            else
            {
                Shield -= damage;

                return null;
            }
        }

        CurrentHp -= damage;
        return Animation.TakeDamage();
    }
    public void Healing(int health)
    {
        if (health < 0)
            throw new ArgumentException();

        CurrentHp += health;
    }

    public void AddShield(int count)
    {
        if (count < 0)
            throw new ArgumentException();

        Shield += count;
    }
    private void RemoveShield()
    {
        Shield = 0;
    }

    public void AddStatus(Status status)
    {
        var findedStatus = _statuses.Find(x => x.GetType() == status.GetType());

        if (findedStatus != null)
        {
            findedStatus.AddValue(status.StatusValue);
            return;
        }

        status.ApplyEffect(this);
        _statuses.Add(status);

        status.StatusFinished += RemoveStatus;
    }
    private IEnumerator TickTempStatuses()
    {
        var tempStatuses = _statuses.OfType<TempStatus>().ToList();

        foreach (var status in tempStatuses)
            yield return status.Tick();

        yield return null;
    }
    private void RemoveStatus(Status status)
    {
        _statuses.Remove(status);
        status.StatusFinished -= RemoveStatus;
    }

    #region CALC VALUE
    public int CalcDamage(Unit target, int damage)
    {
        damage += CurrentStatus.DamageBonus;

        if (CurrentStatus.IsWeak == true)
            damage = Mathf.RoundToInt(damage * 0.75f);

        if (target.CurrentStatus.IsVulnerable == true)
            damage = Mathf.RoundToInt(damage * 1.5f);

        if (damage < 0)
            damage = 0;

        return damage;
    }

    public int CalcShield(int shield)
    {
        shield += CurrentStatus.ShieldBonus;

        if (CurrentStatus.IsFragile == true)
            shield = Mathf.RoundToInt(shield * 0.75f);

        if (shield < 0)
            shield = 0;

        return shield;
    }
    #endregion

    #region USED IN ANIMATOR
    public void DestroyUnit()
    {
        Death?.Invoke(this);
        Destroy(gameObject);
    }
    #endregion
}
