using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField] private GameObject Instructions;
    [SerializeField] private string startText = "repariere den turboantrieb";

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
        Audio.PlayAudio("Hint");
        Instructions.SetActive(true);
        var text = Instructions.GetComponentInChildren<TextMeshProUGUI>();
        text.SetText(startText);
        yield return null;
    }
}
