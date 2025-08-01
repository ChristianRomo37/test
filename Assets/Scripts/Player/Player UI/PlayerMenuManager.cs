using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuManager : MonoBehaviour
{
    [SerializeField] GameObject activeMenu = null;
    [SerializeField] GameObject previousActiveMenu = null;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject returnToMainMenuConfirmation;
    [SerializeField] GameObject pauseMenu;

    public bool isPaused;
    float timeScaleOrig;



    private void Awake()
    {
        timeScaleOrig = Time.timeScale;
    }

    public void SetOptionsMenuActive()
    {
        previousActiveMenu = activeMenu;
        activeMenu = null;
        activeMenu = optionsMenu;
        activeMenu.SetActive(true);
    }

    public void SetPauseMenuActive()
    {
        previousActiveMenu = activeMenu;
        activeMenu = null;
        activeMenu = pauseMenu;
        activeMenu.SetActive(true);
        PauseGame();
        isPaused = !isPaused;
        
       
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void UnpauseGame()
    {
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = !isPaused;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void ReturnToMainMenuPressed()
    {
        previousActiveMenu = activeMenu;
        activeMenu = null;
        activeMenu = returnToMainMenuConfirmation;
        activeMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        UnpauseGame();
        StartCoroutine(WorldSaveGameManager.instance.LoadMainMenu());
    }

    public void ReturnToPreviousMenu()
    {
        if(previousActiveMenu != null)
        {
            activeMenu = null;
            activeMenu = previousActiveMenu;
            activeMenu.SetActive(true);
        }
    }


}
