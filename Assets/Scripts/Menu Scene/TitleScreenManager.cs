using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    public void StartNewGame()
    {
        Instantiate(player);
        StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}