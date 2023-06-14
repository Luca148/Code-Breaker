using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class WaterRoom : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject GameOverUI;
    private PauseMenu pm;

    [Header("Settings")]
    [SerializeField] private Vector3 StartPosition;
    [SerializeField] private Vector3 SlideDirection;
    [SerializeField] private float SlideAmount;
    [SerializeField] private float SlideSpeed;
    [SerializeField] private float SlideSpeedMultiplier = 5;
    private bool startWater = false;
    private BoxCollider bCollider;
    [SerializeField] private BoxCollider termianlCollider;
    [SerializeField] private BoxCollider powerTermianlCollider;
    [SerializeField] private GameObject AIVoiceline;

    [Header("Audio")]
    [SerializeField] private AudioSource waterAudioSource;
    [SerializeField] private AudioClip RoomFill;
    [SerializeField] private AudioClip RoomDrain;
    [SerializeField] private AudioClip RoomFinish;

    [Header("Doors")]
    [SerializeField] private Door door1;
    [SerializeField] private Door door2;
    [SerializeField] private AutoOpenDoor autoDoor2;

    private void Start()
    {
        bCollider = GetComponent<BoxCollider>();
        pm = FindObjectOfType<PauseMenu>();
    }

    private IEnumerator RiseWater()
    {
        Vector3 endPosition = StartPosition + SlideAmount * SlideDirection;
        Vector3 startPosition = water.transform.position;

        waterAudioSource.PlayOneShot(RoomFill);

        float time = 0;
        while (time < 1 && startWater)
        {
            water.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * SlideSpeed;
        }
        if (startWater)
        {
            GameOverUI.SetActive(true);
            pm.FreezeGame();
            FindObjectOfType<AudioManager>().PlayAudio("Quill_Ertrinken");
        }
    }

    private IEnumerator DrowSounds()
    {
        yield return new WaitForSeconds(51);
        if (startWater)
        {
            FindObjectOfType<AudioManager>().PlayAudio("Quill_Einatmen");
        }
        yield return new WaitForSeconds(42);
        if (startWater)
        {
            FindObjectOfType<AudioManager>().PlayAudio("Quill_vorm_Ertrinken");
        }
        yield return null;
    }

    private IEnumerator LowerWater()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = water.transform.position;

        waterAudioSource.PlayOneShot(RoomDrain);

        float time = 0;
        while (time < 1)
        {
            water.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * SlideSpeed * SlideSpeedMultiplier;
        }
        waterAudioSource.Stop();
        waterAudioSource.PlayOneShot(RoomFinish);
        StartCoroutine(EndDialoge());
        water.SetActive(false);
    }

    IEnumerator StartDialoge()
    {
        yield return new WaitForSeconds(3); 
        var Audio = FindObjectOfType<AudioManager>();
        Audio.PlayAudio("Quill_Water");
        yield return new WaitForSeconds(Audio.ReturnClipLength("Quill_Water"));
        Audio.PlayAudio("Dieter_Water");
        yield return new WaitForSeconds(Audio.ReturnClipLength("Dieter_Water") + .5f);
        termianlCollider.enabled = true;
        ChangeInstruction.Change("Benutze das Terminal");
        yield return new WaitForSeconds(5);
        Audio.PlayAudio("KI_Water");
    }

    IEnumerator EndDialoge()
    {
        yield return new WaitForSeconds(3);
        var Audio = FindObjectOfType<AudioManager>();
        Audio.PlayAudio("Dieter_Power");
        yield return new WaitForSeconds(Audio.ReturnClipLength("Dieter_Power") + .5f);
        ChangeInstruction.Change("Schalte den Strom an");
        SaveManager.SetSaveInt(3);
        door1.IsLocked = false;
        door2.IsLocked = false;
        door1.Open(transform.position);
        autoDoor2.source.PlayOneShot(autoDoor2.doorOpen);
        powerTermianlCollider.enabled = true;
        AIVoiceline.SetActive(true);
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartWater();
            StartCoroutine(StartDialoge());
        }
    }

    public void StopWater()
    {
        termianlCollider.enabled = false;
        bCollider.enabled = false;
        startWater = false;
        StartCoroutine(LowerWater());
    }

    private void StartWater()
    {
        door1.IsLocked = true;
        door1.Close();
        door2.IsLocked = true;
        door2.Close();
        startWater = true;
        water.SetActive(true);
        StartPosition = water.transform.position;
        StartCoroutine(RiseWater());
        StartCoroutine(DrowSounds());
        var collider = GetComponent<BoxCollider>();
        collider.enabled = false;
    }
}
