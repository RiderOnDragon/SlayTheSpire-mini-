using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Campfire : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CampPanel _campPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        _campPanel.gameObject.SetActive(true);
        enabled = false;
    }
}
