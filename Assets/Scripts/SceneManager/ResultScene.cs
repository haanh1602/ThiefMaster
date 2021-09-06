using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScene : MonoBehaviour
{
    [SerializeField]
    Sprite win;

    [SerializeField]
    Sprite failed;

    private SpriteRenderer spriteRenderer;

    public void Win()
    {
        spriteRenderer.sprite = win;
    }

    public void Failed()
    {
        spriteRenderer.sprite = failed;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
