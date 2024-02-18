using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitData : ScriptableObject
{
    [SerializeField] protected int _maxHp;

    public int MaxHp { get => _maxHp; }
}
