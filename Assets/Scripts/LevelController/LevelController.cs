using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private Sprite passed;

    [SerializeField]
    private Sprite stucking;

    [SerializeField]
    private Sprite locking;

    [SerializeField]
    public int level;

    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void Start()
    {
        int levelPassed = SavingSystem.LoadPlayer().passedLevel;
        if (level < levelPassed)
        {
            img.sprite = passed;
        }
        else if (level == levelPassed)
        {
            img.sprite = stucking;
        }
        else
        {
            img.sprite = locking;
        }
    }

    public void ChooseLevel()
    {
        int passedLevel = SavingSystem.LoadPlayer().passedLevel;
        //Debug.Log(level + "\n" + passedLevel + ", " + PlayerData.currentLevel);
        if(level <= passedLevel)
        {
            PlayerData.currentLevel = level;
        }
    }
}
