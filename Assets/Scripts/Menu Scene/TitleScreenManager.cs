using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Button optionsButton;
    private PlayerMenuManager playerMenuManager;

    private void Start()
    {
        playerMenuManager = FindObjectOfType<PlayerMenuManager>();

        if(playerMenuManager != null)
        {
            optionsButton.onClick.RemoveAllListeners();
            optionsButton.onClick.AddListener(playerMenuManager.SetOptionsMenuActive);
        }
    }
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