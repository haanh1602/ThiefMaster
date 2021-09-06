using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        
    }

    public void ChooseLevel(int level)
    {
        if(level <= SavingSystem.LoadPlayer().passedLevel)
        {
            ChangeScene("Level" + level.ToString() + "Scene");
        }
    }

    public void Replay()
    {
        ChangeScene("Level" + PlayerData.currentLevel.ToString() + "Scene");
    }

    public void NextLevel()
    {
        if (PlayerData.currentLevel == PlayerData.maxLevel)
        {
            ChangeScene("ChooseLevelScene");
        }
        else if (PlayerData.currentLevel < PlayerData.maxLevel)
        {
            ChangeScene("Level" + (PlayerData.currentLevel + 1).ToString() + "Scene");
        }
    }

    public void ResultScene(bool Win)
    {
        if(Win)
        {
            ChangeScene("WinScene");
            Debug.Log(SceneManager.GetSceneByName("WinScene").GetRootGameObjects().Length);
            GameObject firstRootObject = GameObject.Find("WinSceneImages");
            if (firstRootObject)
            {
                Debug.Log("Found");
            } else {
                firstRootObject = GameObject.Find("GUI");
                if(firstRootObject)
                {
                    Debug.Log("OldScene");
                }
            }

                //SceneManager.GetSceneByName("WinScene").GetRootGameObjects()[0];
            firstRootObject.GetComponent<SpriteRenderer>().sprite = firstRootObject.GetComponent<ResultSceneImages>().GetWinSprite(PlayerData.currentLevel);
        }
        else
        {
            ChangeScene("FailedScene");
            GameObject firstRootObject = SceneManager.GetSceneByName("FailedScene").GetRootGameObjects()[0];
            firstRootObject.GetComponent<SpriteRenderer>().sprite = firstRootObject.GetComponent<ResultSceneImages>().GetFailedSprite(PlayerData.currentLevel);
        }
    }

    public static void ResetData()
    {
        SavingSystem.SavePlayer(new PlayerData(1));
    }

    public void Exit()
    {
        Application.Quit();
    }
}
