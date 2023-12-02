using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    static DataManager instance;
    public static DataManager Instance
    {
        get 
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;

                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }



    string GameDataFileName = "GameData.json";
    string PlayerDataFileName = "PlayerData.json";

    public GameData gData = new GameData();
    public PlayerData pData = new PlayerData();

    public void LoadGameData()
    {
        string gameDataFilePath = Application.persistentDataPath + "/" + GameDataFileName;
        string playerDataFilePath = Application.persistentDataPath+ "/" + PlayerDataFileName;

        if (File.Exists(gameDataFilePath))
        {
            string FromJsonGameData = File.ReadAllText(gameDataFilePath);
            string FromJsonPlayerData = File.ReadAllText(playerDataFilePath);
            gData = JsonUtility.FromJson<GameData>(FromJsonGameData);
            pData = JsonUtility.FromJson<PlayerData>(FromJsonPlayerData);
            print("불러오기 완료");
        }
    }



    public void SaveGameData()
    {
        string ToJsonGameData = JsonUtility.ToJson(gData, true);
        string ToJsonPlayerData = JsonUtility.ToJson(pData, true);

        string gameDataFilePath = Application.persistentDataPath + "/" + GameDataFileName;
        string playerDataFilePath = Application.persistentDataPath + "/" + PlayerDataFileName;



        File.WriteAllText(gameDataFilePath, ToJsonGameData);
        File.WriteAllText(playerDataFilePath, ToJsonPlayerData);

        print("저장 완료");

    }
}
