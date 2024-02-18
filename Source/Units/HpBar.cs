using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private TextMeshProUGUI _hpValueText;

    [Space(10)]

    [SerializeField] private Image _shieldFill;
    [SerializeField] private Image _shieldIcon;
    [SerializeField] private TextMeshProUGUI _shieldValueText;

    public void UpdataHpBar(int currentHp, int maxHp)
    {
        if (currentHp > 0)
        {
            _hpBar.value = (float)currentHp / maxHp;
            _hpValueText.text = $"{currentHp} / {maxHp}";
        }
        else
        {
            _hpBar.gameObject.SetActive(false);
        }
    }

    public void UpdataShieldBar(int shield)
    {
        if (shield > 0)
        {
            _shieldFill.gameObject.SetActive(true);
            _shieldIcon.gameObject.SetActive(true);
            _shieldValueText.text = shield.ToString();
        }
        else
        {
            _shieldFill.gameObject.SetActive(false);
            _shieldIcon.gameObject.SetActive(false);
        }
    }
}
