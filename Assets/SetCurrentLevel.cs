using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SetCurrentLevel : MonoBehaviour
{
    private void Awake()
    {
        string levelSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(levelSceneName);
        levelSceneName = levelSceneName.Replace("Level", "").Replace("Scene","");
        int currentLevel = Int16.Parse(levelSceneName);
        PlayerData.currentLevel = currentLevel;
    }
}
