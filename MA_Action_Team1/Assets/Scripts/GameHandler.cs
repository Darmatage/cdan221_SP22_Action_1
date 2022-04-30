
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour
{

    private GameObject player;
    public static int playerHealth = 100;
    public int StartPlayerHealth = 100;
    public GameObject healthText;

    public static int gotDiamonds = 0;
    public GameObject diamondsText;

	public static bool hasFirePower = false;
	public static bool hasIcePower = false; 
	public static bool hasLightningPower = false;

	public GameObject FirePowerIcon;
	public GameObject IcePowerIcon;
	public GameObject LightningPowerIcon;

    public bool isDefending = false;

    public static bool stairCaseUnlocked = false;
    //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

    private string sceneName;

    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;

    void Awake()
    {
        SetLevel(volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
        if (sliderTemp != null)
        {
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }

    void Start(){
		FirePowerIcon.SetActive(false);
		IcePowerIcon.SetActive(false);
		LightningPowerIcon.SetActive(false);
		
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        updateStatsDisplay();
        pauseMenuUI.SetActive(false);
        GameisPaused = false;
		
		//if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
        playerHealth = StartPlayerHealth;
        //}
    }


    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        volumeLevel = sliderValue;
    }

    public void playerGetDiamonds(int newDiamonds)
    {
        gotDiamonds += newDiamonds;
        updateStatsDisplay();
    }

    public void playerGetHit(int damage)
    {
        if (isDefending == false)
        {
            playerHealth -= damage;
            if (playerHealth >= 0)
            {
                updateStatsDisplay();
            }
            //player.GetComponent<PlayerHurt>().playerHit();
        }

        if (playerHealth >= StartPlayerHealth)
        {
            playerHealth = StartPlayerHealth;
        }

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            playerDies();
        }
    }


	public void GetNewPower(string newPower){
		if (newPower == "fire"){
			hasFirePower = true;
		}
		else if (newPower == "ice"){
			hasIcePower = true;
		}
		else if (newPower == "lightning"){
			hasLightningPower = true;
		}
		updateStatsDisplay();
	}

    public void updateStatsDisplay()
    {
        Text healthTextTemp = healthText.GetComponent<Text>();
        healthTextTemp.text = "HEALTH: " + playerHealth;

        Text diamondsTextTemp = diamondsText.GetComponent<Text>();
        diamondsTextTemp.text = "DIAMONDS: " + gotDiamonds;
		
		if (hasFirePower == true){FirePowerIcon.SetActive(true);} 
		else {FirePowerIcon.SetActive(false);}

		if (hasIcePower == true){IcePowerIcon.SetActive(true);} 
		else {IcePowerIcon.SetActive(false);}

		if (hasLightningPower == true){LightningPowerIcon.SetActive(true);} 
		else {LightningPowerIcon.SetActive(false);}
		
    }

    public void playerDies()
    {
        //player.GetComponent<PlayerHurt>().playerDead();
        StartCoroutine(DeathPause());
    }

    IEnumerator DeathPause()
    {
        player.GetComponent<PlayerMoveAround>().isAlive = false;
        //player.GetComponent<PlayerJump>().isAlive = false;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("SceneLose");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
        playerHealth = StartPlayerHealth;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}