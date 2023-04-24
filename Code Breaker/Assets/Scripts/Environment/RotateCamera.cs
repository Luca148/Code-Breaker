using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] float Speed;

    void Update()
    {
        transform.Rotate(Vector3.up * Speed * Time.deltaTime);
    }
}
