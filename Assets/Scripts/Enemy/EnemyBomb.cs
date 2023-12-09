using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    [SerializeField] private GameObject Effect;

    LineRenderer lineRenderer;
    public Transform attackStartPoint;
    public float maxDistance = 100f;

    private void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();

        lineRenderer.material.color = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        AttackLine();
    }
    private void Update()
    {

    }
    void AttackLine()
    {
        // �������� ��ǥ
        Vector3 startPosition = attackStartPoint.position;

        // �������� ��ǥ (���������� y�� �������� maxDistance��ŭ ������)
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + maxDistance, startPosition.z);

        // �������� �������� ���� ����
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);

        // ������ Ȱ��ȭ
        lineRenderer.enabled = true;

        // �浹 üũ
        CheckCollision(startPosition, endPosition);

        // 0.1�� �Ŀ� ������ ��Ȱ��ȭ
        Invoke("DisableAttackEffect", 1f);
    }

    void CheckCollision(Vector3 start, Vector3 end)
    {
        // Raycast�� ����Ͽ� �浹 üũ
        RaycastHit hit;
        if (Physics.Raycast(start, Vector3.down, out hit, maxDistance))
        {
            // �浹 �������� ������ ����
            lineRenderer.SetPosition(1, hit.point);

            // ���⿡�� hit.collider �� ����Ͽ� �浹�� ��ü�� ���� ó�� ����
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
        }
    }

    void DisableAttackEffect()
    {
        lineRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {

            PlayerHealth player= other.GetComponent<PlayerHealth>();
            // �÷��̾� ������

            Instantiate(Effect, new Vector3(transform.position.x, transform.position.y - 12f, transform.position.z)
    , Quaternion.identity);

            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Ground")
        {
            Instantiate(Effect, new Vector3(transform.position.x, transform.position.y - 12f, transform.position.z)
    , Quaternion.identity);

            Destroy(this.gameObject);
        }

    }
}
