using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cardCount;

    public void UpdateView(int cardCount)
    {
        _cardCount.text = cardCount.ToString();
    }
}
