using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    private void Awake()
    {
        if(PlayerData.currentLevel <= SavingSystem.LoadPlayer().passedLevel && PlayerData.currentLevel < PlayerData.maxLevel)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
