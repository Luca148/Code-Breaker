using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGameManager : MonoBehaviour
{
    private Fade fade;
    [SerializeField] private HackingTerminal terminal;

    private void Start()
    {
        terminal = null;
        fade = FindObjectOfType<Fade>();
        terminal = FindObjectOfType<HackingTerminal>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            terminal.StartEndAnim();
            FindObjectOfType<AudioManager>().PlayAudio("HackGame_Finish");
        }
    }
}
