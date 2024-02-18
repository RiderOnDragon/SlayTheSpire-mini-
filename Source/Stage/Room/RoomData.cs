using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomData : ScriptableObject
{
    public enum Type
    {
        SIMPLE_ENEMY,
        CAMP
    }

    public virtual Type RoomType { get; }

    public abstract void LoadRoom();
}
