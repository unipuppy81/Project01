using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : SingletonType<GameManager>
{
    [SerializeField]
    private GameObject SceneManager;

    public Vector3 characterPosition;

    public GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player01");
    }

    private void Update()
    {
        /*
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.SetActive(true);
            characterPosition = player.transform.position;
            Debug.Log(characterPosition.ToString());
            LoadingSceneManager.nextScene = "Stage1";
            //GameManager.Instance.characterPosition = characterTransform.position;
        }
        */
    }
}
