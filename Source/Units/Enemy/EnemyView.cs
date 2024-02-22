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

    public void ChangeActionView(Ability.Type action, int value)
    {
        switch (action) 
        {
            case Ability.Type.DEAL_DAMAGE:
                _actionImage.sprite = _attackSprite;
                break;
            case Ability.Type.ADD_SHIELD:
                _actionImage.sprite = _shieldSprite;
                break;
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
