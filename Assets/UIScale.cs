using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScale : MonoBehaviour
{

    CanvasScaler cs;

    private void Awake()
    {
        cs = GetComponent<CanvasScaler>();
        cs.referenceResolution = new Vector2(Screen.width, Screen.height);
    }

}
