using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCompletor : MonoBehaviour
{
    [SerializeField] private Teleport _teleportPrefab;
    [SerializeField] private Transform _teleportContainer;
    [SerializeField] private Vector2 _teleportPositionOffset;

    public static RoomCompletor Singleton;

    public static event Action CompletedRoom;

    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(this);
    }

    public void CompleteRoom()
    {
        var nextRooms = BasicRoomLoadingOptions.StageTempData.GetNextRooms();

        //Убрать отсюда и добавить отдельную финальную сцену
        if (nextRooms == null)
        {
            Debug.Log("Вы прошли игру!");
            IJunior.TypedScenes.MainMenu.Load();
            return;
        }

        int i = 0;
        foreach (var room in nextRooms)
        {
            var teleport = Instantiate(_teleportPrefab, _teleportContainer);
            teleport.transform.localPosition = _teleportPositionOffset * i;
            teleport.Init(room);
            i++;
        }

        CompletedRoom?.Invoke();
    }
}
