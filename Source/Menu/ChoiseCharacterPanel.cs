using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiseCharacterPanel : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Button _choiseCharacterButtonPrefab;
    [SerializeField] private Transform _buttonContainer;

    private void Awake()
    {
        foreach (var characterData in _mainMenu.CharacterDatas)
        {
            var button = Instantiate(_choiseCharacterButtonPrefab, _buttonContainer);
            button.onClick.AddListener(() =>
            {
                _mainMenu.StartGame(characterData);
            });
        }
    }
}
