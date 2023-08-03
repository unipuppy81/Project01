using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllor : MonoBehaviour
{
    public GameObject target;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

   

    private void LateUpdate()
    {
        Vector3 FixedPos = new Vector3(target.transform.position.x + offsetX, target.transform.position.y + offsetY, target.
            transform.position.z + offsetZ);
        transform.position = FixedPos;
    }
}
