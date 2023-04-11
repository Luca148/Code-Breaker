using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private float WaitTime = 8;
    [SerializeField] private bool toPerdita = true;

    [SerializeField] private Fade fade;

    void Start()
    {
        StartCoroutine(LoadNextScene());
        fade = GameObject.Find("LevelLoader").GetComponent<Fade>();
    }

    //this loads the next scene after waittime is over
    //if toperdita = true then transition to pedita
    //else transition to hub
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(WaitTime);
        if (toPerdita)
        {
            StartCoroutine(fade.LevelLoader("Perdita"));
        }
        else
        {
            StartCoroutine(fade.LevelLoader("HUB"));
        }
        yield return null;
    }
}
