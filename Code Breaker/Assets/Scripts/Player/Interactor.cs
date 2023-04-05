using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public LayerMask interactMask = 8;
    [SerializeField] Image Crosshair;
    [SerializeField] RectTransform CrosshairTransform;
    [SerializeField] Sprite crosshairSprite;
    [SerializeField] Sprite interactSprite;
    [SerializeField] Color defaultColor;
    [SerializeField] Color interactColor;
    public bool canInteract = true;
    UnityEvent onInteract;

    private void Start()
    {
        Crosshair.color = defaultColor;
    }

    void Update()
    {
        RaycastHit hit;

        //Raycast for objects with interact layer
        //when hit get Interactable component and change crosshair
        //if playerinput = e then trigger event
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2, interactMask))
        {
            if (canInteract)
            {
                if (hit.collider.GetComponent<Interactable>() != null)
                {
                    onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                    CrosshairTransform.sizeDelta = new Vector2(60, 60);
                    Crosshair.color = interactColor;
                    Crosshair.sprite = interactSprite;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        onInteract.Invoke();
                    }
                }
            }
        }
        else //change chrosshair to default
        {
            if(Crosshair.color != defaultColor)
            {
                CrosshairTransform.sizeDelta = new Vector2(20, 20);
                Crosshair.color = defaultColor;
                Crosshair.sprite = crosshairSprite;
            }
        }
    }
}
