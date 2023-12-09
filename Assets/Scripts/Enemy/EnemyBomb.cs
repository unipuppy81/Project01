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
        // 시작점의 좌표
        Vector3 startPosition = attackStartPoint.position;

        // 종료점의 좌표 (시작점에서 y축 방향으로 maxDistance만큼 떨어짐)
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + maxDistance, startPosition.z);

        // 레이저의 시작점과 끝점 설정
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);

        // 레이저 활성화
        lineRenderer.enabled = true;

        // 충돌 체크
        CheckCollision(startPosition, endPosition);

        // 0.1초 후에 레이저 비활성화
        Invoke("DisableAttackEffect", 1f);
    }

    void CheckCollision(Vector3 start, Vector3 end)
    {
        // Raycast를 사용하여 충돌 체크
        RaycastHit hit;
        if (Physics.Raycast(start, Vector3.down, out hit, maxDistance))
        {
            // 충돌 지점까지 레이저 조절
            lineRenderer.SetPosition(1, hit.point);

            // 여기에서 hit.collider 를 사용하여 충돌한 객체에 대한 처리 가능
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
            // 플레이어 데미지

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
