using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneImages : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> winImages;

    [SerializeField]
    private List<Sprite> failedImages;

    private SpriteRenderer spriteRenderer;

    public Sprite GetWinSprite(int level)
    {
        return winImages[level - 1];
    }

    public Sprite GetFailedSprite(int level)
    {
        return failedImages[level - 1];
    }

    public Sprite GetSprite(int level, bool Win)
    {
        if (Win) return GetWinSprite(level);
        else return GetFailedSprite(level);
    }

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        if(spriteRenderer != null)
        {
            if (gameObject.name.Equals("WinSceneImages"))
            {
                spriteRenderer.sprite = GetWinSprite(PlayerData.currentLevel);
                Debug.Log("Win");
            } else if (gameObject.name.Equals("FailedSceneImages"))
            {
                spriteRenderer.sprite = GetFailedSprite(PlayerData.currentLevel);
                Debug.Log("Failed");
            }
        }
    }
}
