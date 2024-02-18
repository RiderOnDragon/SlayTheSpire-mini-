using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _img;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _abilityDescription;
    [SerializeField] private Image _manaCostImg;
    [SerializeField] private TextMeshProUGUI _manaCostText;

    private CardData _data;

    public void Init(CardData data)
    {
        _data = data;

        _background.sprite = _data.Background;
        _img.sprite = _data.Img;

        _name.text = _data.Name;
        _abilityDescription.text = _data.Description;

        _manaCostImg.sprite = _data.ManaCostImg;
        _manaCostText.text = _data.ManaCost.ToString();
    }
}
