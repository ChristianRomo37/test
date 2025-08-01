using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine;

public class PlayerMenuManager : MonoBehaviour
{
    

    [SerializeField] public GameObject activeMenu = null;
    [SerializeField] public GameObject previousActiveMenu = null;
    [SerializeField] public GameObject optionsMenu;
    [SerializeField] public GameObject returnToMainMenuConfirmation;
    [SerializeField] public GameObject pauseMenu;

    public bool isPaused;
    float timeScaleOrig;



    private void Awake()
    {
        timeScaleOrig = Time.timeScale;
       
    }

    private void Update()
    {
        InputSystem.Update();
    }
    public void SetOptionsMenuActive()
    {
        SetPreviousMenu();
        if (activeMenu != null)
        {
            activeMenu.SetActive(false);
        }
        activeMenu = optionsMenu;
        activeMenu.SetActive(true);
    }

    public void SetPauseMenuActive()
    {
        if(WorldSaveGameManager.instance.GetCurrentSceneIndex() != 0)
        {
           isPaused = !isPaused;
           SetPreviousMenu();
           if(activeMenu != null)
           {
              activeMenu.SetActive(false);
           }
           activeMenu = pauseMenu;
           activeMenu.SetActive(true);
           PauseGame();
        }
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
        previousActiveMenu = null;
    }

    public void ReturnToMainMenuPressed()
    {
        SetPreviousMenu();
        activeMenu.SetActive(false);
        activeMenu = returnToMainMenuConfirmation;
        activeMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        UnpauseGame();
        StartCoroutine(WorldSaveGameManager.instance.LoadMainMenu());
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ReturnToPreviousMenu()
    {
        if(previousActiveMenu != null)
        {
            activeMenu.SetActive(false);
            activeMenu = previousActiveMenu;
            activeMenu.SetActive(true);
            previousActiveMenu = null;
        }
    }

    public void SetPreviousMenu()
    {
        if(activeMenu != null)
        {
            previousActiveMenu = activeMenu;
            previousActiveMenu.SetActive(false);
        }
    }


}
