using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVoiceline : MonoBehaviour
{
    [SerializeField] private string VoiceLineName;

    private BoxCollider bCollider;

    private void Start()
    {
        bCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartVoiceLine());
        }
    }

    IEnumerator StartVoiceLine()
    {
        bCollider.enabled = false;
        var Audio = FindObjectOfType<AudioManager>();
        Audio.PlayAudio(VoiceLineName);
        yield return new WaitForSeconds(Audio.ReturnClipLength(VoiceLineName) + .5f);
        ChangeInstruction.Change("Durchsuche den Raum nach dem Code");
        yield return null;
    }
}
