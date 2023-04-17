using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGameManager : MonoBehaviour
{
    private Fade fade;
    private HackingTerminal terminal;
    [SerializeField] private string LevelName;

    private void Start()
    {
        fade = FindObjectOfType<Fade>();
        terminal = FindObjectOfType<HackingTerminal>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            terminal.StartEndAnim();
        }
    }
}
