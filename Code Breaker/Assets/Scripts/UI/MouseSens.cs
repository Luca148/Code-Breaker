using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MouseSens : MonoBehaviour
{
    [SerializeField] Slider sensSlider;
    [SerializeField] TextMeshProUGUI count;
    [SerializeField] string _sensParameter = "sensitivity";

    private void Update()
    {
        count.text = sensSlider.value.ToString("F1");
    }

    private void OnDisable() //saves float of volume slider to playerprefs
    {
        PlayerPrefs.SetFloat(_sensParameter, sensSlider.value);
    }

    private void Start()
    {
        sensSlider.value = PlayerPrefs.GetFloat(_sensParameter, sensSlider.value); //sets save volume slider float as volume slider float
    }
}
