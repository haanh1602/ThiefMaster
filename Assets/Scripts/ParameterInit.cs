using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterInit : MonoBehaviour
{
    public float originHeight = 1440;
    public float originWidth = 2960;
    public float height = 1440;
    public float width = 2960;
    public float scale = 1;

    private void Awake()
    {
        height = Screen.currentResolution.width;
        width = Screen.currentResolution.height;
        scale = width / originWidth;
        Debug.Log(scale);
    }
}
