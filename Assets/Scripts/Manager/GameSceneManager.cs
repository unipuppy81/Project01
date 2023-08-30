using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : SingletonType<GameSceneManager>
{
    [SerializeField]
    private GameObject sceneControl;
    private Player player;
    private Scene currentScene;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player")) 
        { 
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        currentScene = SceneManager.GetActiveScene();

        Invoke("Init", 0.02f);

    }

    private void Update()
    {
        /*
        if (Input.GetKeyUp(KeyCode.Space))
        {
            sceneControl.SetActive(true);
            LoadingSceneManager.nextScene = "Stage1";
            //GameManager.Instance.characterPosition = characterTransform.position;
        }
        */
    }
    private void Init()
    {
        PlayerData.LoadData();
    }


    public Player Player { get { return player; } }
    public Scene Scene { get { return currentScene; } }
}
