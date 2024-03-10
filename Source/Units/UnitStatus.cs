using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatus
{
    [HideInInspector] public int DamageBonus = 0;
    [HideInInspector] public int ShieldBonus = 0;
    [HideInInspector] public bool IsVulnerable = false;
    [HideInInspector] public bool IsWeak = false;
    [HideInInspector] public bool IsFragile = false;
}
