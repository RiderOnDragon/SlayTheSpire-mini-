using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Slider _manaBar;
    [SerializeField] private TextMeshProUGUI _manaValueText;

    public void UpdateBar(int currentMana, int maxMana)
    {
        _manaBar.value = (float)currentMana / maxMana;
        _manaValueText.text = currentMana.ToString();
    }
}
