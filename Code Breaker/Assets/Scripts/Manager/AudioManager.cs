using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; //array of sounds

    /*
    FindObjectOfType<AudioManager>().PlayAudio("test_sound");
    */

    void Awake()
    {
        foreach (Sound s in sounds) //foreach object in array
        {
            s.source = gameObject.AddComponent<AudioSource>(); //create audiosource
            s.source.clip = s.clip; //Der Sound als Datei

            s.source.outputAudioMixerGroup = s.outputAudioMixerGroup; //audimixer for volume

            s.source.volume = s.volume; //volume of the sound
            s.source.pitch = s.pitch; //pitch of the sound
            s.source.loop = s.loop; //loop sound
        }
    }
    
    public void PlayAudio (string name) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //search array for name
        if (s == null) //if the is no sound with this name then 
        {
            Debug.LogWarning("Sound: " + name + " not found!"); //send warning
            return;
        }
        s.source.Play(); //play sound
    }

    public void StopAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //search array for name
        if (s == null) //if the is no sound with this name then 
        {
            Debug.LogWarning("Sound: " + name + " not found!"); //send warning
            return;
        }
        s.source.Stop(); //stop sound
    }
}
