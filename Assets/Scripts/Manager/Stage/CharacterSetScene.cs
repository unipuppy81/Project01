using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetScene : MonoBehaviour
{
    public float rotationSpeed = 5f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");

            transform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);
        }
    }
}
