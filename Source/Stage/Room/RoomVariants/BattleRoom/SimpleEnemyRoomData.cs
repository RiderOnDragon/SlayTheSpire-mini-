using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleEnemyRoomData", menuName = "Data/Stage/Room/Create SimpleEnemyRoomData")]
public class SimpleEnemyRoomData : BattleRoomData
{
    public override Type RoomType { get; } = Type.SIMPLE_ENEMY;
}
