using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoomInitializer : RoomInitializer, ISceneLoadHandler<BattleRoomData>
{
    [SerializeField] private EnemySquad _enemySquad;

    [Space(10)]

    [SerializeField] private AvailableCardsDeck _availableCardsDeck;

    [Space(10)]

    [SerializeField] private Hand _hand;

    [Space(10)]


    private BattleRoomData _roomData;

    public BattleRoomData RoomData { get => _roomData; }

    
    public static BattleRoomInitializer Singleton;

    public void OnSceneLoaded(BattleRoomData roomData)
    {
        _roomData = roomData;
    }

    protected override void RoomInit()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(this);

        _enemySquad.Init(_roomData.EnemyWave);

        List<CardData> deck = new List<CardData>(BasicRoomLoadingOptions.CharacterData.Deck);
        _availableCardsDeck.Init(deck);

        _hand.Init();
    }
}
