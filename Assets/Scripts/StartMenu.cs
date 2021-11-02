using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    GameObject menu;
    GameObject helpUI;
    GameObject settingsUI;

    GameObject musicON;
    GameObject musicOFF;

    public static bool musicIsOn = true;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("UI_StartMenu");
        helpUI = GameObject.FindGameObjectWithTag("UI_Help");
        settingsUI = GameObject.FindGameObjectWithTag("UI_Settings");
        musicON = GameObject.FindGameObjectWithTag("ON");
        musicOFF = GameObject.FindGameObjectWithTag("OFF");

        menu.SetActive(true);
        helpUI.SetActive(false);
        settingsUI.SetActive(false);

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }

    public void Help()
    {
        helpUI.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settingsUI.SetActive(true);
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
            sound.Stop();
        }
        else
        {
            // -> Musik anschalten
            musicON.SetActive(true);
            musicOFF.SetActive(false);
            musicIsOn = true;
            sound.Play();
        }
    }

    // public bool getMusicIsOn()
    // {
    //     return musicIsOn;
    // }

    public static bool MusicIsOn
    { 
        get { return musicIsOn; }
        //set { name = value; }
    }

}

   