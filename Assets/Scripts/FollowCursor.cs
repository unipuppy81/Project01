using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private bool isCursorOn = false;

    void Update()
    {
        if (isCursorOn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit))
            {
                Vector3 nextVec = new Vector3(rayHit.point.x, 1.1f, rayHit.point.z);
                transform.position = nextVec;        
            }  
        }
    }

    private void OnEnable()
    {
        isCursorOn = true;
    }

    private void OnDisable()
    {
        isCursorOn = false;
    }
}


