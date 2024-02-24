using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(HpBar), typeof(UnitAnimation))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] private HpBar _hpBar;
    [SerializeField] private Collider2D _mainCollider;

    protected UnitAnimation _animation;
    protected UnitData _data;

    private List<Status> _statuses = new List<Status>();

    private int _currentHp;
    private int _shield;

    protected int CurrentHp 
    {
        get { return _currentHp; }
        set {
            if (value <= 0)
            {
                _mainCollider.enabled = false;
                _animation.Death();
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

    public event Action<Unit> Death;

    public void Init(UnitData data)
    {
        _animation = GetComponent<UnitAnimation>();

        _data = data;
        _animation.Init();

        ChildInit();
    }

    protected abstract void ChildInit();
    protected virtual void ChildOnNextTurn() { }

    protected void OnNextTurn()
    {
        TickTempStatuses();
        RemoveShield();

        ChildOnNextTurn();
    }

    public void TakeDamage(int damage, bool ignoreShield = false)
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

                return;
            }
        }

        _animation.TakeDamage();
        CurrentHp -= damage;
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
        _statuses.Add(status);
        Debug.Log(_statuses.Count);

        status.StatusFinished += RemoveStatus;
    }
    private void TickTempStatuses()
    {
        var tempStatuses = _statuses.OfType<TempStatus>().ToList();

        foreach (var status in tempStatuses)
            status.Tick();
    }
    private void RemoveStatus(Status status)
    {
        _statuses.Remove(status);
        status.StatusFinished -= RemoveStatus;
    }

    //Used in Animator
    public void DestroyUnit()
    {
        Death?.Invoke(this);
        Destroy(gameObject);
    }
}
