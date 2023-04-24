using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerController pc;
    public ThirdPersonController thirdPersonController;
    public GameObject cam;

    public static bool GameIsPaused = false;
    public bool isOptionsOpen = false;

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject optionsUI;
    [SerializeField] GameObject Instructions;

    public bool CantPause = false;

    [SerializeField] private Fade fade;

    [SerializeField] private AudioSource[] audioSources;

    public void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        fade = FindObjectOfType<Fade>();

        audioSources = FindObjectsOfType<AudioSource>();
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
        Instructions.SetActive(true);

        if(cam != null)
        {
            cam.SetActive(true);
        }

        if (thirdPersonController != null)
        {
            thirdPersonController.disableInput = false;
            thirdPersonController.lockCursor = true;
        }
        pc.disableInput = false; //enable input
        pc.lockCursor = true; //lockcursor

        ResumeAudio();
    }

    private void ResumeAudio()
    {
        for(int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].UnPause();
        }
    }

    void Pause() //pause game
    {
        Cursor.visible = true; //enable cursor
        Time.timeScale = 0f; //stop time
        GameIsPaused = true; //game is paused
        pauseUI.SetActive(true); //active pause menu ui
        Instructions.SetActive(false);

        if (cam != null)
        {
            cam.SetActive(false);
        }

        if (thirdPersonController != null)
        {
            thirdPersonController.disableInput = true;
            thirdPersonController.lockCursor = false;
        }
        pc.disableInput = true; //enable input
        pc.lockCursor = false; //lockcursor

        PauseAudio();
    }

    private void PauseAudio()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].Pause();
        }
    }

    public void LoadMenu() //load back to main menu
    {
        Time.timeScale = 1f; //resume time
        Cursor.visible = true; //enable cursor
        if (thirdPersonController != null)
        {
            thirdPersonController.disableInput = true;
            thirdPersonController.lockCursor = false;
        }
        pc.disableInput = true; //enable input
        pc.lockCursor = false; //lockcursor
        StartCoroutine(fade.LevelLoader("MainMenu")); //laod main menu
    }

    public void FreezeGame()
    {
        Cursor.visible = true; //enable cursor
        Time.timeScale = 0f; //stop time
        Instructions.SetActive(false);

        if (cam != null)
        {
            cam.SetActive(false);
        }

        if (thirdPersonController != null)
        {
            thirdPersonController.disableInput = true;
            thirdPersonController.lockCursor = false;
        }
        pc.disableInput = true; //enable input
        pc.lockCursor = false; //lockcursor
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

    public void ResetScene()
    {
        Time.timeScale = 1f; //resume time
        Cursor.visible = true; //enable cursor
        if (thirdPersonController != null)
        {
            thirdPersonController.disableInput = true;
            thirdPersonController.lockCursor = false;
        }
        pc.disableInput = true; //enable input
        pc.lockCursor = false; //lockcursor
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.UnloadSceneAsync("Hacking Test 1");
    }

    public void SetFullscreen(bool isFullscreen) //toggle fullscreen of the game 
    {
        Screen.fullScreen = isFullscreen; //change fullscreen mode with bool of button*
    }
}
