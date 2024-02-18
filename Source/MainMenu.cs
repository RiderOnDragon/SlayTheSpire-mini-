using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData; //Позже сделать окно выбора персонажа
    [SerializeField] private StageConfig _stageConfig;
    [SerializeField] private StageTempData _stageTempData;
    [SerializeField] private TempCharacterData _tempCharacterData;

    public void StartGame()
    {
        _stageTempData.Init(_stageConfig);
        _tempCharacterData.Init(_characterData);

        new BasicRoomLoadingOptions(_stageTempData, _tempCharacterData);
        _stageTempData.GetStartRoom().LoadRoom();
    }

    public void OpenSettingsPanel()
    {
        Debug.Log("Settings Panel Opened");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
