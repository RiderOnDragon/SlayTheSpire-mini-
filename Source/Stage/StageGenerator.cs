using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageGenerator
{
    private RoomData _startRoom;
    private List<List<RoomData>> _roomsOnStage = new List<List<RoomData>>();

    private StageConfig _config;

    private const int START_ROOM_INDEX = 0;

    public RoomData StartRoom { 
        get {
            if (_startRoom != null)
                return _startRoom;
            else
                throw new System.Exception("It is impossible to access the data because the generation has not been performed");
        } 
    }
    public List<List<RoomData>> RoomsOnStage {
        get
        {
            if (_roomsOnStage.Count > 0)
                return _roomsOnStage;
            else
                throw new System.Exception("It is impossible to access the data because the generation has not been performed");
        }
    }

    public StageGenerator(StageConfig config)
    {
        _config = config;
    }


    public void GenerateStage()
    {
        _startRoom = GetStartRoom();

        for (int i = START_ROOM_INDEX + 1; i < _config.Levels.Count; i++)
            _roomsOnStage.Add(GetRoomsOnLevel(i));
    }

    public RoomData GetStartRoom()
    {
        var rooms = GetRoomsOnLevel(START_ROOM_INDEX);
        int roomIndex = Random.Range(0, rooms.Count);
        var startRoom = rooms[roomIndex];

        return startRoom;
    }

    public List<RoomData> GetRoomsOnLevel(int levelNumber)
    {
        var level = _config.Levels[levelNumber];

        List<RoomData> roomsData = new List<RoomData>();

        foreach (var variant in level.RoomTypeOnLevel)
        {
            var rooms = _config.AllRooms.First(room => room.Type == variant);
            int roomIndex = Random.Range(0, rooms.Variats.Count);
            var room = rooms.Variats[roomIndex];
            roomsData.Add(room);
        }

        return roomsData;
    }
}
