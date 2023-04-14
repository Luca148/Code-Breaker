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

    [Header("Doors")]
    [SerializeField] private Door door1;
    [SerializeField] private Door door2;

    bool startWater;

    void Update()
    {
    }

    private IEnumerator RiseWater()
    {
        Vector3 endPosition = StartPosition + SlideAmount * SlideDirection;
        Vector3 startPosition = water.transform.position;

        float time = 0;
        while (time < 1)
        {
            water.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * SlideSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartWater();
        }
    }

    private void StartWater()
    {
        door1.IsLocked = true;
        door1.Close();
        door2.IsLocked = true;
        door2.Close();

        water.SetActive(true);
        startWater = true;
        StartPosition = water.transform.position;
        StartCoroutine(RiseWater());
    }
}
