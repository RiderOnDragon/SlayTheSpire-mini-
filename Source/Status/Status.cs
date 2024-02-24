using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status
{
    private int _value;

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

    public event Action<Status> StatusFinished;

    public Status(Unit target, int value)
    {
        _value = value;

        ApplyEffect(target);
    }

    protected abstract void ApplyEffect(Unit taget);
}