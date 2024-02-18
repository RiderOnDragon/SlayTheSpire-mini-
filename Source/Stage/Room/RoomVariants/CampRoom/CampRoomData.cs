using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CampRoomData", menuName = "Data/Stage/Room/Create CampRoomData")]
public class CampRoomData : RoomData
{
    public override Type RoomType { get; } = Type.CAMP;

    public override void LoadRoom()
    {
        CampRoom.Load();
    }
}
