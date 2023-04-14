using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingTerminal : MonoBehaviour
{
    public Fade fade;
    [SerializeField] private string LevelName;

    private void Start()
    {
        fade = FindObjectOfType<Fade>();
    }

    public void StartMinigame()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);

        Debug.Log("Start Minigame");

        StartCoroutine(fade.LevelLoader(LevelName));
    }
}
