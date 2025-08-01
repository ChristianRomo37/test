using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("----- Scene -----")]
    public Scene context;


    [Header("-----Player Stuff-----")]
    public GameObject player;
    //public playerControler playerScript;
    public GameObject playerSpawnPos;


    [Header("-----Enemy Stuff-----")]





    [Header("-----UI Stuff-----")]
    //public UIElements ui;
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject winMenu;
    public GameObject playerDamageFlash;
 
    public GameObject ret;
    public Button respawn;


    [Header("----- HUD Stuff-----")]
    public TextMeshProUGUI ePrompt;
    public TextMeshProUGUI aPrompt;
    public TextMeshProUGUI totalMagSize;
    public TextMeshProUGUI bulletsLeft;
    public Image HPBar;
    public TextMeshProUGUI reloadPrompt;
    public TextMeshProUGUI objectivePrompt;
  

    [Header("----- Main Menu -----")]
    public GameObject levelSelect;
    public GameObject settings;
    public GameObject confirmManager;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        
    }

    void Update()
    {
        
    }

    public void pauseState()
    {

    }

    public void unPauseState()
    {
        
    }

    public void youLose()
    {
        
    }



}
