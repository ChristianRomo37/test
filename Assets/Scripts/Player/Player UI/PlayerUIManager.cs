using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    [SerializeField] public PlayerUIHudManager playerUIHudManager;
    [HideInInspector] public PlayerMenuManager playerMenuManager;

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

        playerMenuManager = GetComponentInChildren<PlayerMenuManager>();
        


    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (WorldSaveGameManager.instance.GetCurrentSceneIndex() == WorldSaveGameManager.instance.GetMainSceneIndex())
        {
            playerUIHudManager.gameObject.SetActive(false);
        }
        else
        {
            playerUIHudManager.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += OnSceneChanged;

        if (WorldSaveGameManager.instance.GetCurrentSceneIndex() == WorldSaveGameManager.instance.GetMainSceneIndex())
        {
            playerUIHudManager.gameObject.SetActive(false);
        }
        else
        {
            playerUIHudManager.gameObject.SetActive(true);
        }

    }


}
