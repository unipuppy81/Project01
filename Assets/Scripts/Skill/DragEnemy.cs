using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEnemy : MonoBehaviour
{
    LayerMask cubeLayer;
    public float radius = 10.0f;

    [SerializeField] GameObject Effect;
    void Start()
    {
        cubeLayer = LayerMask.NameToLayer("Enemy");

        GameObject newEffect = Instantiate(Effect, new Vector3(this.transform.position.x, this.transform.position.y + 1, 
            this.transform.position.z), Quaternion.identity);

        Destroy(newEffect, 5.0f);
        Destroy(this.gameObject, 5.0f);
        
    }

    void Update()
    {
        int layerMask = (1 << cubeLayer);

        Collider[] colliders =
                    Physics.OverlapSphere(this.transform.position, radius, layerMask);

        foreach (Collider col in colliders)
        {
            if (col.name == "Sphere" /* 자기 자신은 제외 */) continue;

            

            if (col.tag == "Enemy")
            {
                col.gameObject.GetComponent<EnemyBase>().isDragging = true;
                col.gameObject.GetComponent<EnemyBase>().DragPosition = this.transform.position;
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.transform.position, radius);
    }

    private void OnDisable()
    {
        int layerMask = (1 << cubeLayer);

        Collider[] colliders =
                    Physics.OverlapSphere(this.transform.position, radius, layerMask);

        foreach (Collider col in colliders)
        {
            if (col.name == "Sphere" /* 자기 자신은 제외 */) continue;



            if (col.tag == "Enemy")
            {
                col.gameObject.GetComponent<EnemyBase>().isDragging = false;
                //col.gameObject.GetComponent<Enemy>().DragPosition = this.transform.position;
            }
        }

    }
}

