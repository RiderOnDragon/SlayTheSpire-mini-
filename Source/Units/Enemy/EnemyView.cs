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

    public void ChangeActionView(ActionPatterns.Type action, int value)
    {
        switch (action) 
        {
            case ActionPatterns.Type.WEAK_ATTACK:
                _actionImage.sprite = _attackSprite;
                break;
            case ActionPatterns.Type.SHIELD:
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
