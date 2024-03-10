using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status
{
    private int _value;
    protected Unit _target;

    protected int Value
    {
        get { return _value; }
        set 
        { 
            if (value <= 0)
            {
                StatusFinished?.Invoke(this);
                return;
            }

            _value = value; 
        }
    }

    public int StatusValue { get => _value; }


    public event Action<Status> StatusFinished;

    public Status(int value)
    {
        _value = value;
    }

    public void ApplyEffect(Unit target)
    {
        _target = target;
        UseInitialEffect();
    }

    protected virtual void UseInitialEffect() { }
    protected virtual void RemoveStatus() { }

    public void AddValue(int value)
    {
        if (_target == null)
            throw new System.ArgumentNullException();
        if (value < 0)
            throw new System.ArgumentException();

        _value += value;
        ApplyEffect(_target);
    }
}