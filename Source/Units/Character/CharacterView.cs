using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    private ManaBar _manaBar;

    public void Init()
    {
        _manaBar = FindObjectOfType<ManaBar>();
    }

    public void UpdateManaBar(int currentMana, int maxMana)
    {
        _manaBar?.UpdateBar(currentMana, maxMana);
    }
}
