using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int passedLevel = 1;
    public static int currentLevel = 1;
    public static readonly int maxLevel = 10;

    public PlayerData() { }

    public PlayerData(int passedLevel)
    {
        this.passedLevel = passedLevel;
    }
}
