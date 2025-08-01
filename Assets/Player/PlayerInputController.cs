using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController instance;

    PlayerControlls playerControlls;

    [SerializeField] public bool pauseInput = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OneSceneChange(Scene oldScene, Scene newScene)
    {
        //IF WE ARE LOADING INTO OUR WORLD SCENE, ENABLE OUR PLAYERS CONTROLS
        if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
        {
            instance.enabled = true;
        }
        //OTHERWISE WE MUST BE AT THE MAIN MENU, DISABLE OUR PLAYERS CONTROLS
        //THIS IS SO OUR PLAYER CANT MOVE AROUND IF WE ENTER THINGS LIKE A CHARACTER CREATION MENU ECT
        else
        {
            instance.enabled = false;
        }
    }

    private void OnDestroy()
    {
        //IF WE DESTROY THIS OBJECT, UNSUBSCRIBE FROM THIS EVENT
        SceneManager.activeSceneChanged -= OneSceneChange;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += OneSceneChange;

        instance.enabled = false;
    }

    private void OnEnable()
    {
        if(playerControlls == null)
        {
            playerControlls = new PlayerControlls();

            playerControlls.PlayerActions.Pause.performed += i => pauseInput = true;
        }

        playerControlls.Enable();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (enabled)
        {
            if (focus)
            {
                playerControlls.Enable();
            }
            else
            {
                playerControlls.Disable();
            }

        }
    }

    private void Update()
    {
        //InputSystem.Update();
        HandleAllInputs();
        
    }

    private void HandleAllInputs()
    {
        HandlePauseInput();
    }

    private void HandlePauseInput()
    {
        if(pauseInput == true)
        {
            if (PlayerUIManager.instance.playerMenuManager.activeMenu == null)
            {
                PlayerUIManager.instance.playerMenuManager.SetPauseMenuActive();
            }
            else if (PlayerUIManager.instance.playerMenuManager.activeMenu != null && PlayerUIManager.instance.playerMenuManager.previousActiveMenu != null)
            {
                PlayerUIManager.instance.playerMenuManager.ReturnToPreviousMenu();
            }
            else if (PlayerUIManager.instance.playerMenuManager.activeMenu == PlayerUIManager.instance.playerMenuManager.pauseMenu)
            {
                PlayerUIManager.instance.playerMenuManager.UnpauseGame();
            }
            else if(PlayerUIManager.instance.playerMenuManager.previousActiveMenu == null && PlayerUIManager.instance.playerMenuManager.activeMenu != null)
            {
                PlayerUIManager.instance.playerMenuManager.activeMenu.SetActive(false);
                PlayerUIManager.instance.playerMenuManager.activeMenu = null;
            }

            pauseInput = false;

        }
        
    }
}
