using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Introduction : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(StartIntroduction());
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
