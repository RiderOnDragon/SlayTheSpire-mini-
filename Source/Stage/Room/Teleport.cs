using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TeleportView))]
public class Teleport : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TeleportView _view;
    private RoomData _nextRoom;

    public void Init(RoomData nextRoom)
    {
        _nextRoom = nextRoom;
        _view.Init(_nextRoom.RoomType);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _nextRoom.LoadRoom();
    }
}
