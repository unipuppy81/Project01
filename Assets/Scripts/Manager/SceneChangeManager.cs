using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public GameObject ExplainPanel;
    public Image loadingImage;

    bool isLoad;

    void Start()
    {
        isLoad = false;
    }

    void Update()
    {
        if (isLoad)
        {
            loadingImage.fillAmount += Time.deltaTime;

            if (loadingImage.fillAmount >= 1)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }
    public void Load()
    {
        isLoad = true;
        
    }



    public void ExplainEnter()
    {
        ExplainPanel.SetActive(true);
    }

    public void ExplainExit()
    {
        ExplainPanel.SetActive(false);
    }
    
}
