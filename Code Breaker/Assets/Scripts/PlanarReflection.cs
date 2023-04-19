using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PlanarReflection : MonoBehaviour
{
    [SerializeField] PlanarReflectionProbe probe;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            probe.RequestRenderNextUpdate();
        }
    }
}
