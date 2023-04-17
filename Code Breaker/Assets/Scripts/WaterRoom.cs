using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class WaterRoom : MonoBehaviour
{
    [SerializeField] private GameObject water;

    [Header("Settings")]
    [SerializeField] private Vector3 StartPosition;
    [SerializeField] private Vector3 SlideDirection;
    [SerializeField] private float SlideAmount;
    [SerializeField] private float SlideSpeed;
    [SerializeField] private float SlideSpeedMultiplier = 5;
    private bool startWater = false;
    private BoxCollider bCollider;

    [Header("Doors")]
    [SerializeField] private Door door1;
    [SerializeField] private Door door2;

    private void Start()
    {
        bCollider = GetComponent<BoxCollider>();
    }

    private IEnumerator RiseWater()
    {
        Vector3 endPosition = StartPosition + SlideAmount * SlideDirection;
        Vector3 startPosition = water.transform.position;

        float time = 0;
        while (time < 1 && startWater)
        {
            water.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * SlideSpeed;
        }
        //KILL PLAYER
    }

    private IEnumerator LowerWater()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = water.transform.position;

        float time = 0;
        while (time < 1)
        {
            water.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * SlideSpeed * SlideSpeedMultiplier;
        }

        door1.IsLocked = false;
        door2.IsLocked = false;
        door2.Open(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartWater();
        }
    }

    public void StopWater()
    {
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
    }
}
