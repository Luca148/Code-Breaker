using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Save
{
    public string Phase;
    public Vector3 PlayerSpawnPosition;
    public Quaternion PlayerSpawnRotation;
    public GameObject[] gameObjectsToActive;
    public UnityEvent Trigger;
    public bool changeInstruction;
    public string Instruction;
}
