using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HackingTerminal : MonoBehaviour
{
    public UnityEvent Trigger;

    [SerializeField] private string LevelName;
    [SerializeField] private GameObject HackingDevice;
    [SerializeField] private GameObject Crosshair;
    private PlayerController pc;
    [SerializeField] private Animator anim;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    public void StartEndAnim()
    {
        anim.SetTrigger("End");
    }

    public void FinishedMinigame()
    {
        SceneManager.UnloadSceneAsync(LevelName);
        pc.enabled = true;
        HackingDevice.SetActive(false);
        Crosshair.SetActive(true);

        Trigger.Invoke();
    }

    public void StartMinigame()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        pc.enabled = false;
        HackingDevice.SetActive(true);
        Crosshair.SetActive(false);
        FindObjectOfType<AudioManager>().PlayAudio("HackGame_Start");
        SceneManager.LoadSceneAsync(LevelName, LoadSceneMode.Additive);
    }
}
