using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Image _actionImage;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Sprite _attackSprite;
    [SerializeField] private Sprite _shieldSprite;

    public void ChangeActionView(Ability action, int value)
    {
        switch (action)
        {
            case DealDamageAbility:
                _actionImage.sprite = _attackSprite;
                break;
            case AddShieldAbility:
                _actionImage.sprite = _shieldSprite;
                break;
            default:
                throw new System.Exception("The raw type of ability");
        }

        _valueText.text = value.ToString();

        _actionImage.gameObject.SetActive(true);
        _valueText.gameObject.SetActive(true);
    }

    public void DisableActionView()
    {
        _actionImage.gameObject.SetActive(false);
        _valueText.gameObject.SetActive(false);
    }
}
