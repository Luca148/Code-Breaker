using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] bool isThirdPerson = false;
    public PlayerController pc;
    public ThirdPersonController thirdPersonController;

    public static bool GameIsPaused = false;
    public bool isOptionsOpen = false;

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject optionsUI;

    public bool CantPause = false;

    [SerializeField] private Fade fade;

    public void Start()
    {
        if (isThirdPerson)
        {
            thirdPersonController = FindObjectOfType<ThirdPersonController>();
        }
        else
        {
            pc = FindObjectOfType<PlayerController>();
        }
        fade = FindObjectOfType<Fade>();
    }

    //if player can pause game and player hit esc then
    //open pause menu
    //else resume game
    //if options are open then
    //close options
    void Update()
    {
        if (!CantPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused && !isOptionsOpen)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }

                if (isOptionsOpen)
                {
                    closeOptions();
                }
            }
        }
    }

    public void Resume() //resume game
    {
        Cursor.visible = false; //disable cursor
        Time.timeScale = 1f;  //resume time
        GameIsPaused = false; //game is not paused
        pauseUI.SetActive(false); //disable ui

        if (isThirdPerson)
        {
            thirdPersonController.disableInput = false;
            thirdPersonController.lockCursor = true;
        }
        else
        {
            pc.disableInput = false; //enable input
            pc.lockCursor = true; //lockcursor
        }
    }

    void Pause() //pause game
    {
        Cursor.visible = true; //enable cursor
        Time.timeScale = 0f; //stop time
        GameIsPaused = true; //game is paused
        pauseUI.SetActive(true); //active pause menu ui

        if (isThirdPerson)
        {
            thirdPersonController.disableInput = true;
            thirdPersonController.lockCursor = false;
        }
        else
        {
            pc.disableInput = true; //enable input
            pc.lockCursor = false; //lockcursor
        }
    }

    public void LoadMenu() //load back to main menu
    {
        Time.timeScale = 1f; //resume time
        Cursor.visible = true; //enable cursor
        if (isThirdPerson)
        {
            thirdPersonController.disableInput = true;
            thirdPersonController.lockCursor = false;
        }
        else
        {
            pc.disableInput = true; //enable input
            pc.lockCursor = false; //lockcursor
        }
        StartCoroutine(fade.LevelLoader("MainMenu")); //laod main menu
    }

    public void openOptions() //show options
    {
        optionsUI.SetActive(true); //activate options menu
        pauseUI.SetActive(false); //deactive pause ui
        isOptionsOpen = true; //options are open
    }

    public void closeOptions() //close options
    {
        optionsUI.SetActive(false); //deactivate options ui
        pauseUI.SetActive(true); //active pause ui
        isOptionsOpen = false; //options are closed
    }

    public void SetFullscreen(bool isFullscreen) //toggle fullscreen of the game 
    {
        Screen.fullScreen = isFullscreen; //change fullscreen mode with bool of button*
    }
}