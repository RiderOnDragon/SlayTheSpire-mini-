using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampRoomInitializer : RoomInitializer
{
    [SerializeField] private CampPanel _campPanel;

    protected override void RoomInit()
    {
        _campPanel.Init();
    }
}
