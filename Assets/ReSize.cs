using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSize : MonoBehaviour
{
    float width, height;
    float xScale, yScale;
    float ScreenWidth, ScreenHeight;

    [SerializeField]
    private Sprite background;

    [SerializeField]
    private GameObject g;

    private Transform myTransform;

    private void Awake()
    {
/*        Screen.fullScreenMode = new FullScreenMode();
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        Camera camera = GameObject.Find("Size/Main Camera").GetComponent<Camera>() ;
*//*        ScreenWidth = 720;
        ScreenHeight = 1520;*//*
        width = background.rect.width;
        height = background.rect.height;
        xScale = ScreenWidth / width;
        yScale = ScreenHeight / height;
        Debug.Log(ScreenWidth + ", " + ScreenHeight);
        Debug.Log(width + ", " + height);
        float scale = Mathf.Min(xScale, yScale);
        myTransform = GetComponent<Transform>();
        myTransform.localScale = new Vector3(scale, scale, 1);*/
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
