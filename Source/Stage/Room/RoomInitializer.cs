using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomInitializer : MonoBehaviour
{
    [SerializeField] private Transform _characterPosition;

    private void Awake()
    {
        Init();
        RoomInit();
    }

    protected abstract void RoomInit();

    private void Init()
    {
        var characterData = BasicRoomLoadingOptions.CharacterData;
        var character = Instantiate(characterData.Prefab, _characterPosition);
        character.Init(characterData);
    }
}
