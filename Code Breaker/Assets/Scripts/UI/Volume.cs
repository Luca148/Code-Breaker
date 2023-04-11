using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] string _volumeParameter = "volume";
    public float volume;

    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(HandleSliderValueChanged); //gets value from slider
    }

    private void HandleSliderValueChanged(float value)
    {
        audioMixer.SetFloat(_volumeParameter, value); //set float of slider to audiomixer "volume"
    }

    private void OnDisable() //saves float of volume slider to playerprefs
    {
        PlayerPrefs.SetFloat(_volumeParameter, volumeSlider.value);
    }

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(_volumeParameter, volumeSlider.value); //sets save volume slider float as volume slider float
    }
}
