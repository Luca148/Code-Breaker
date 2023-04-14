using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGameManager : MonoBehaviour
{
    private Fade fade;
    [SerializeField] private string LevelName;

    private void Start()
    {
        fade = FindObjectOfType<Fade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(fade.LevelLoader(LevelName));
        }
    }
}
