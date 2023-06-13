using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Options;
    public GameObject Main;
    [SerializeField] private string NextSceneName = "SceneName";

    [SerializeField] private Fade fade;

    public void Start()
    {
        fade = FindObjectOfType<Fade>();
    }

    //play intro if first time playing then load hub
    public void PlayGame()
    {
        {
            StartCoroutine(fade.LevelLoader(NextSceneName));
        }
    }

    public void QuitGame()
    {
        Application.Quit(); //quit game
    }

    //this opens the option menu at the start of the game
    public void OpenOptions()
    {
        Options.SetActive(true);
        Main.SetActive(false);
    }

    public void OpenMainMenu() //closes opiton menu then open main menu
    {
        Options.SetActive(false); 
        Main.SetActive(true);
    }

    public void SetFullscreen (bool isFullscreen) //toggle fullscreen of the game 
    {
        Screen.fullScreen = isFullscreen; //change fullscreen mode with bool of button
    }

    public void ResetGame()
    {
        SaveManager.SetSaveInt(0);
    }
}
