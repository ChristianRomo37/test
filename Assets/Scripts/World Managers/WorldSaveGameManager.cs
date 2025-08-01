using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance;

    [Header("World Scene Index")]
    [SerializeField] int worldSceneIndex = 1;
    [SerializeField] int mainMenuSceneIndex = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator LoadWorldScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

        yield return null;
    }

    public int GetWorldSceneIndex()
    {
        return worldSceneIndex;
    }

    public int GetMainSceneIndex()
    {
        return mainMenuSceneIndex;
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public IEnumerator LoadMainMenu()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(mainMenuSceneIndex);

        yield return null;
    }
}
