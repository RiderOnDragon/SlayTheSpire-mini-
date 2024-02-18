using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsDatabase", menuName = "Data/Card/Create CardsDatabase")]
public class CardsDatabase : ScriptableObject
{
    [SerializeField] private SerializedDictionary<CharacterData.CharacterClass, List<CardData>> _cards = new SerializedDictionary<CharacterData.CharacterClass, List<CardData>>();

    public SerializedDictionary<CharacterData.CharacterClass, List<CardData>> Cards { get => _cards; }
}
