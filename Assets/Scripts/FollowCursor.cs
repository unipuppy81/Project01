using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    private Player player;

    [SerializeField] private bool isCursorOn = false;

    private void Start()
    {
        player = GameObject.Find("Player01").GetComponent<Player>();
    }

    void Update()
    {
        if (isCursorOn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit))
            {
                Vector3 nextVec = new Vector3(rayHit.point.x, player.transform.position.y + 2.0f, rayHit.point.z);
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


