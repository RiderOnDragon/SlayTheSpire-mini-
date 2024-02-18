using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageTempData", menuName = "Data/Stage/Create TempStageData")]
public class StageTempData : ScriptableObject
{
    private RoomData _startRoom;
    private List<List<RoomData>> _roomsOnStage = new List<List<RoomData>>();

    private int _level;

    public void Init(StageConfig stageConfig)
    {
        _level = -1;

        var generator = new StageGenerator(stageConfig);
        generator.GenerateStage();

        _startRoom = generator.StartRoom;
        _roomsOnStage = generator.RoomsOnStage;
    }

    public RoomData GetStartRoom()
    {
        return _startRoom;
    }

    public List<RoomData> GetNextRooms()
    {
        _level++;

        if (_level >= _roomsOnStage.Count)
            return null;

        return _roomsOnStage[_level];
    }
}
