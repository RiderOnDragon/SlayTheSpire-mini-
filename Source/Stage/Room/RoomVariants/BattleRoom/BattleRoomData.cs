using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;


public abstract class BattleRoomData : RoomData
{
    [SerializeField] private List<EnemyWave> enemyWave = new List<EnemyWave>();

    public List<EnemyWave> EnemyWave { get => enemyWave; }

    public override void LoadRoom()
    {
        BattleRoom.Load(this);
    }
}