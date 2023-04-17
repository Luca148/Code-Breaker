using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpenDoor : MonoBehaviour
{
    Door door;

    private void Start()
    {
        door = GetComponentInChildren<Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            if (door != null)
            {
                if (!door.IsOpen)
                {
                    door.Open(other.transform.position);
                    Debug.Log("Open");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            if (door != null)
            {
                if (door.IsOpen)
                {
                    door.Close();
                    Debug.Log("Close");
                }
            }
        }
    }
}
