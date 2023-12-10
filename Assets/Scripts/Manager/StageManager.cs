using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    Transform[] stage1Spawn;

    [SerializeField]
    GameObject tankPrefab;


    [SerializeField]
    AnimatorController specificAnimatorController;


    
    void Start()
    {
        Transform spawnGroup = GameObject.Find("Spawn").transform;
        stage1Spawn = new Transform[spawnGroup.childCount];
        for (int i = 0; i < spawnGroup.childCount; i++)
        {
            stage1Spawn[i] = spawnGroup.GetChild(i);

        }


        foreach (Transform t in stage1Spawn)
        {
            GameObject newEnemy = Instantiate(tankPrefab, t.position, Quaternion.identity);
            Animator animator = newEnemy.GetComponentInChildren<Animator>();
            animator.runtimeAnimatorController = specificAnimatorController;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
