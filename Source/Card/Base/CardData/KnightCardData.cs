using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Data/Card/Knight/Create CardData")]
public class KnightCardData : CardData
{
    public override CharacterData.CharacterClass Affiliation { get; } = CharacterData.CharacterClass.KNIGHT;
}
