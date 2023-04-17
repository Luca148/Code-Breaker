using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackingTerminal : MonoBehaviour
{
    private Fade fade;
    [SerializeField] private string LevelName;
    [SerializeField] private GameObject HackingDevice;
    [SerializeField] private GameObject Crosshair;
    private PlayerController pc;


    private void Start()
    {
        fade = FindObjectOfType<Fade>();
        pc = FindObjectOfType<PlayerController>();
    }

    public void StartMinigame()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        pc.enabled = false;
        HackingDevice.SetActive(true);
        Crosshair.SetActive(false);

        SceneManager.LoadSceneAsync(LevelName, LoadSceneMode.Additive);
    }
}
