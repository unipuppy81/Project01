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

            // �þ߿� �ִ��� üũ
            if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
            {
                // �þ߿� ������ ü�¹ٸ� Ȱ��ȭ�ϰ� ��ġ ������Ʈ
                hpBarList[i].SetActive(true);
                hpBarList[i].transform.position = screenPos;
            }
            else
            {
                // �þ߿� ������ ü�¹ٸ� ��Ȱ��ȭ
                hpBarList[i].SetActive(false);
            }

            //hpBarList[i].transform.position = cam.WorldToScreenPoint(objectList[i].position + new Vector3(0, 1.15f, 0));
        }
    }
}
