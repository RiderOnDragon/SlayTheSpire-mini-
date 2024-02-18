using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionPatterns
{
    public enum Type
    {
        WEAK_ATTACK,
        SHIELD
    }

    [SerializeField] private Type _type;
    [SerializeField] private int _value;


    public Type ActionType { get => _type; }
    public int Value { get => _value; }
}
