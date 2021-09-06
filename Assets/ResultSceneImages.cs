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

    public Sprite getWinSprite(int level)
    {
        return winImages[level - 1];
    }

    public Sprite getFailedSprite(int level)
    {
        return failedImages[level - 1];
    }

    public Sprite getSprite(int level, bool Win)
    {
        if (Win) return getWinSprite(level);
        else return getFailedSprite(level);
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
                spriteRenderer.sprite = getWinSprite(PlayerData.currentLevel);
                Debug.Log("Win");
            } else if (gameObject.name.Equals("FailedSceneImages"))
            {
                spriteRenderer.sprite = getFailedSprite(PlayerData.currentLevel);
                Debug.Log("Failed");
            }
        }
    }
}
