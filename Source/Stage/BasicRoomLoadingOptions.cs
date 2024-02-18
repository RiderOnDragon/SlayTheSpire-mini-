using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BasicRoomLoadingOptions
{
    public static StageTempData StageTempData { get; private set; }
    public static TempCharacterData CharacterData { get; private set; }

    public BasicRoomLoadingOptions(StageTempData stageTempData, TempCharacterData characterData)
    {
        if (StageTempData == null && CharacterData == null)
        {
            StageTempData = stageTempData;
            CharacterData = characterData;
        }
        else
        {
            throw new System.Exception($"struct {nameof(BasicRoomLoadingOptions)} has already been created earlier");
        }
    }
}