using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> remove;

    [SerializeField]
    List<GameObject> requiredItem;

    [SerializeField]
    List<GameObject> change;

    public void Win()
    {
        Remove();
        RemoveRequiredItem();
        for (int i = 0; i < change.Count; i++)
        {
            change[i].GetComponent<ResultScene>().Win();
        }
    }

    public void Failed()
    {
        Remove();
        for (int i = 0; i < change.Count; i++)
        {
            change[i].GetComponent<ResultScene>().Failed();
        }
    }

    public void Remove()
    {
        for(int i = 0; i < remove.Count; i++)
        {
            remove[i].SetActive(false);
        }
    }

    public void RemoveRequiredItem()
    {
        for(int i = 0; i < requiredItem.Count; i++)
        {
            requiredItem[i].SetActive(false);
        }
    }
}
