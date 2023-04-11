using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

public class Fade : MonoBehaviour
{
    [SerializeField] private Animator transition;

    [SerializeField] private float transitionTime = 1;

    //transition between scenes with fade to black
    public IEnumerator LevelLoader(string Scenename)
    {
        transition.SetTrigger("Start"); //start anim

        yield return new WaitForSeconds(transitionTime); //wait for transitionTime

        SceneManager.LoadScene(Scenename); //load next scene with string name of scene
    }
}
