using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<CharacterData> _characterDatas = new List<CharacterData>();
    [SerializeField] private StageConfig _stageConfig;
    [SerializeField] private StageTempData _stageTempData;
    [SerializeField] private TempCharacterData _tempCharacterData;
    [SerializeField] private GameOver _gameOverBehaivor;

    public List<CharacterData> CharacterDatas { get => _characterDatas; }

    public void StartGame(CharacterData characterData)
    {
        _stageTempData.Init(_stageConfig);
        _tempCharacterData.Init(characterData);

        var character = Instantiate(characterData.Prefab);
        character.Init(_tempCharacterData);
        character.gameObject.SetActive(false);

        _gameOverBehaivor.Init();

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
