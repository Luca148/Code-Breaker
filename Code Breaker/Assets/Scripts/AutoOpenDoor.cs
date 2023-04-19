using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpenDoor : MonoBehaviour
{
    Door door;
    public AudioClip doorOpen;
    [SerializeField] AudioClip doorClose;
    public AudioSource source;

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
                if (!door.IsOpen && !door.IsLocked)
                {
                    door.Open(other.transform.position);
                    source.PlayOneShot(doorOpen);
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
                if (door.IsOpen && !door.IsLocked)
                {
                    door.Close();
                    source.PlayOneShot(doorClose);
                }
            }
        }
    }
}
