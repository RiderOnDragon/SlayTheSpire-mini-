using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private static GameOver _singleton;

    public void Init()
    {
        if (_singleton == null)
        {
            _singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);


        Character.Singleton.Death += OnCharecterDeath;
    }

    private void OnCharecterDeath(Unit unit)
    {
        Character.Singleton.Death -= OnCharecterDeath;

        IJunior.TypedScenes.MainMenu.Load();
        Debug.Log("Поражение");
    }
}
