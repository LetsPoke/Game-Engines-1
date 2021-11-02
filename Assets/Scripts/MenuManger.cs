using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManger : MonoBehaviour
{
    GameObject menu;
    GameObject helpUI;
    GameObject settingsUI;

    GameObject musicON;
    GameObject musicOFF;
    
    bool gameIsPaused = false;
    bool musicIsOn;
    
    public StartMenu startmenu;
    GameObject player;

    AudioSource audioGame;
    AudioSource audioPausenMenu;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("UI_StartMenu");
        helpUI = GameObject.FindGameObjectWithTag("UI_Help");
        settingsUI = GameObject.FindGameObjectWithTag("UI_Settings");
        musicON = GameObject.FindGameObjectWithTag("ON");
        musicOFF = GameObject.FindGameObjectWithTag("OFF");
        player = GameObject.FindGameObjectWithTag("Player");
    
        menu.SetActive(false);
        helpUI.SetActive(false);
        settingsUI.SetActive(false);

        audioGame = player.GetComponent<AudioSource>();
        audioPausenMenu = GetComponent<AudioSource>();
        audioPausenMenu.mute = true;
        
        musicIsOn = StartMenu.MusicIsOn; 

        audioGame.Play();
        audioPausenMenu.Play();
        if (!musicIsOn)
        {
           audioGame.mute = true;
        }
    }

    // Update is called once per frame
    void Update()
    {   

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                {
                    Resume(); 
                }
                else   // pause
                {
                    menu.SetActive(true);
                    Time.timeScale = 0;
                    gameIsPaused = true;
                    if(musicIsOn) 
                    {
                        audioGame.mute = true;
                        audioPausenMenu.mute = false;
                    }
                }
            }


    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        if(musicIsOn) 
        {
            audioGame.mute = false;
            audioPausenMenu.mute = true;
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        gameIsPaused = false;
        if(musicIsOn) 
        {
            audioGame.mute = false;
            audioPausenMenu.mute = true;
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Help()
    {
        helpUI.SetActive(true);
    }

    public void Settings()
    {
        settingsUI.SetActive(true);
        if (musicIsOn) {
            musicON.SetActive(true);
        }
        else{
            musicOFF.SetActive(true);
        }
    }

    // Settings & Help UIS
    public void Home()
    {
        settingsUI.SetActive(false);
        helpUI.SetActive(false);
    }

    public void Music()
    {
        if(musicIsOn)
        {
            // -> Musik ausschalten
            musicOFF.SetActive(true);
            musicON.SetActive(false);
            musicIsOn = false;  
            Debug.Log("musik ist aus");
        }
        else
        {
            // -> Musik anschalten
            musicON.SetActive(true);
            musicOFF.SetActive(false);
            musicIsOn = true; 
            Debug.Log("musik ist an");
        }
    }

}

   