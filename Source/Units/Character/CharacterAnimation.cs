using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : UnitAnimation
{
    private const string ATTACK = "Attack";

    public void Attack()
    {
        PlayAnimation(ATTACK);
    }
}
