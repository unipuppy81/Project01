using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HpBarEnemyS : MonoBehaviour
{
    [SerializeField] GameObject barPrefab = null;
    
    List<Transform> objectList = new List<Transform>();
    List<GameObject> hpBarList = new List<GameObject>();

    Camera cam = null;
    void Start()
    {
        cam = Camera.main;

        GameObject[] targetObject = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < targetObject.Length; i++)
        {
            objectList.Add(targetObject[i].transform);
            GameObject targetHpBar = Instantiate(barPrefab, objectList[i].transform.position, Quaternion.identity, transform);
            hpBarList.Add(targetHpBar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i= 0; i < objectList.Count; i++)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(objectList[i].position + new Vector3(0, 1.15f, 0));

            // 시야에 있는지 체크
            if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
            {
                // 시야에 있으면 체력바를 활성화하고 위치 업데이트
                hpBarList[i].SetActive(true);
                hpBarList[i].transform.position = screenPos;
            }
            else
            {
                // 시야에 없으면 체력바를 비활성화
                hpBarList[i].SetActive(false);
            }

            //hpBarList[i].transform.position = cam.WorldToScreenPoint(objectList[i].position + new Vector3(0, 1.15f, 0));
        }
    }
}
