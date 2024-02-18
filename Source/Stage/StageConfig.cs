using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "StageConfig", menuName = "Data/Stage/Create StageConfig")]
public class StageConfig : ScriptableObject
{
    [SerializeField] private List<Rooms> _allRooms = new List<Rooms>();
    [SerializeField] private List<Level> _levels = new List<Level>();

    public IList<Rooms> AllRooms { get => _allRooms.AsReadOnly(); }
    public IList<Level> Levels { get => _levels.AsReadOnly(); }

    private void OnValidate()
    {
        for (int i = 0; i < _allRooms.Count; i++)
        {
            var roomsType = _allRooms[i].Type;

            if (_allRooms.FindAll(rooms => rooms.Type == roomsType).Count > 1)
                throw new Exception("The type of rooms should not be repeated. Otherwise, only the first type found will be used");
        }

        foreach (var room in AllRooms)
            room.Validate();

        foreach (var level in Levels)
            level.Validate();
    }

    [System.Serializable]
    public struct Level
    {
        [SerializeField] private List<RoomData.Type> _roomTypeOnLevel;

        public HashSet<RoomData.Type> RoomTypeOnLevel { get => _roomTypeOnLevel.ToHashSet(); }

        public void Validate()
        {
            for (int i = 0; i < _roomTypeOnLevel.Count; i++)
            {
                var roomsType = _roomTypeOnLevel[i];

                if (_roomTypeOnLevel.FindAll(type => type == roomsType).Count > 1)
                    throw new Exception("The type of rooms should not be repeated. The presence of a duplicate type will not be taken into account when generating");
            }
        }
    }

    [System.Serializable]
    public struct Rooms
    {
        [SerializeField] private RoomData.Type _type;
        [SerializeField] private List<RoomData> _variats;

        public RoomData.Type Type { get => _type; }
        public IList<RoomData> Variats { get => _variats.AsReadOnly(); }

        public void Validate()
        {
            foreach (var room in Variats)
            {
                if (room != null && room.RoomType != _type)
                {
                    _variats.Remove(room);
                    throw new Exception($"The added point: {room} - belongs to a different type");
                }
            }

            HashSet<RoomData> points = new HashSet<RoomData>();
            List<RoomData> dublicatePoint = _variats.Where(point => points.Add(point) == false).ToList();

            if (dublicatePoint.Count > 0)
            {
                _variats = _variats.Distinct().ToList();
                throw new Exception($"Duplicate elements: {string.Join(", ", dublicatePoint.Select(x => x.ToString()))} - have been deleted");
            }
        }
    }
}
