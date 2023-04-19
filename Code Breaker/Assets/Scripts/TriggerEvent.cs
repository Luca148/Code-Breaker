using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent U_event;
    [SerializeField] private AudioSource source;
    [SerializeField] AudioClip[] sounds = default;

    public void Event()
    {
        U_event.Invoke();
    }

    public void PlaySound()
    {
        source.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    }
}
