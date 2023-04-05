using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; //name of sound

    public AudioClip clip; //sound clip data

    public AudioMixerGroup outputAudioMixerGroup; //audiomixer for volume

    [Range(0f, 1f)]
    public float volume; //valome of sound
    [Range(.1f, 3f)]
    public float pitch; //pitch of sound

    public bool loop; //loop sound

    [HideInInspector]
    public AudioSource source; //created audiosource of sound
}
