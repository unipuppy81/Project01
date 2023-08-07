using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack01 : MonoBehaviour
{
    [SerializeField] private GameObject Effect;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(Effect, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
