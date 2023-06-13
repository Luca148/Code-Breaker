using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    private bool hideControls = false;
    public GameObject Controls;

    private PlayerController pc;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        pc.disableInput = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hideControls)
        {
            StartCoroutine(StartIntroduction());
            hideControls = true;
            Controls.SetActive(false);
            pc.disableInput = false;
        }
    }

    IEnumerator StartIntroduction()
    {
        yield return new WaitForSeconds(1);
        var Audio = FindObjectOfType<AudioManager>();
        Audio.PlayAudio("Dieter_Start");
        yield return new WaitForSeconds(Audio.ReturnClipLength("Dieter_Start"));
        Audio.PlayAudio("Quill_Start");
        yield return new WaitForSeconds(Audio.ReturnClipLength("Quill_Start") + .5f);
        ChangeInstruction.Change("repariere den turboantrieb");
        yield return null;
    }
}
