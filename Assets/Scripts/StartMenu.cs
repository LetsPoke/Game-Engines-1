using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    GameObject startMenu;
    GameObject helpUI;
    GameObject settingsUI;

    GameObject musicON;
    GameObject musicOFF;

    // Start is called before the first frame update
    void Start()
    {
        // Starting MainMenu
        startMenu = GameObject.FindGameObjectWithTag("UI_StartMenu");
        helpUI = GameObject.FindGameObjectWithTag("UI_Help");
        settingsUI = GameObject.FindGameObjectWithTag("UI_Settings");

            startMenu.SetActive(true);
            helpUI.SetActive(false);
            settingsUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // START MENU
    public void StartGame()
    {
       SceneManager.LoadScene("SampleScene");
    }

    public void Help()
    {
        helpUI.SetActive(true);
    }

    public void Quit()
    {
        // TODO: scene destroyn
        Application.Quit();
    }

    public void Settings()
    {
        settingsUI.SetActive(true);
        
        musicON = GameObject.FindGameObjectWithTag("ON");
        musicOFF = GameObject.FindGameObjectWithTag("OFF");

            musicON.SetActive(true); // TODO: Error not instance of current object
            musicOFF.SetActive(false);    
    }

    // Settings & Help UIS
    public void Home()
    {
        settingsUI.SetActive(false);
        helpUI.SetActive(false);
    }

    public void MusicON()
    {
        musicOFF.SetActive(true);
        musicON.SetActive(false);
        // TODO: musik im spiel AUS
    }

    public void MusicOFF()
    {
        musicON.SetActive(true);
        musicOFF.SetActive(false);
        // TODO: musik im spiel AN
       
    }


    


}
