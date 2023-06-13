using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProbeTexture : MonoBehaviour
{
    public ReflectionProbe probe;
    public Cubemap Blue;
    public Cubemap Purple;
    void Start()
    {
        probe = GetComponent<ReflectionProbe>();
        probe.customBakedTexture = Purple;
    }

    private void OnDisable()
    {
        probe.customBakedTexture = Blue;
    }
}
