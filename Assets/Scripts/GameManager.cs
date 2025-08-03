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
    public RewindManager rewindManager;
    public AudioSource playerAudioSource;
    public GameObject akimboArm;


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


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");

        player.transform.position = playerSpawnPos.transform.position;

        rewindManager = player.GetComponentInChildren<RewindManager>();
        
        playerAudioSource = player.GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
