using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerVoiceline : MonoBehaviour
{
    [SerializeField] private string VoiceLineName;
    [SerializeField] private string VoiceLineResponse;

    private BoxCollider bCollider;
    [SerializeField] private bool hasResponse;
    [SerializeField] private bool changeInstruction;
    [SerializeField] private string Instruction;
    public UnityEvent Trigger;

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

        if (hasResponse)
        {
            Audio.PlayAudio(VoiceLineResponse);
            yield return new WaitForSeconds(Audio.ReturnClipLength(VoiceLineResponse) + .5f);
        }

        if (changeInstruction)
        {
            ChangeInstruction.Change(Instruction);
        }
        Trigger.Invoke();
        yield return null;
    }
}
