using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HpBar))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] private HpBar _hpBar;
    [SerializeField] private Collider2D _mainCollider;

    private int _currentHp;
    private int _shield;

    protected abstract UnitAnimation Animation { get; }

    protected UnitData _data;

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

    public event Action<Unit> Death;

    public void Init(UnitData data)
    {
        _data = data;
        Animation.Init();

        ChildInit();
    }

    protected abstract void ChildInit();

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentException();

        if (Shield > 0)
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

        Animation.TakeDamage();
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

    protected void RemoveShield()
    {
        Shield = 0;
    }

    public void DestroyUnit()
    {
        Death?.Invoke(this);
        Destroy(gameObject);
    }
}
