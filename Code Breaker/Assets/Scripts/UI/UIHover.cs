using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHover : MonoBehaviour
{
    [SerializeField] RectTransform img;
    private Vector3 originalScale;
    [SerializeField] private float scaleFactor = 1.2f;

    void Start()
    {
        img = GetComponent<RectTransform>();
        originalScale = img.localScale;
    }

    //add this to the eventtrigger onEnter to change to color of text on hover
    public void OnHoverEnter()
    {
        Vector3 newScale = originalScale * scaleFactor;
        img.localScale = newScale;
    }

    //add this to the eventtrigger onExit to change to color of text back to default
    public void OnHoverExit()
    {
        img.localScale = originalScale;
    }
}
