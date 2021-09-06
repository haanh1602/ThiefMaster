using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleToolPoints : MonoBehaviour
{

    public List<GameObject> child;

    [SerializeField]
    private float Radius;

    private void Awake()
    {
        Transform[] childArr = GetComponentsInChildren<Transform>();
        foreach (Transform children in childArr)
        {
            child.Add(children.gameObject);
        }
        // Remove this object out of childList
        child.RemoveAt(0);
        int n = child.Count;
        float radianUnit = 2 * Mathf.PI / n;
        // start from Vectorx.right (0,1,x)
        for(int i = 0; i < n; i++)
        {
            child[i].transform.localPosition =  new Vector3(Mathf.Cos(radianUnit * i) * Radius, Mathf.Sin(radianUnit * i) * Radius, child[i].transform.position.z);
        }
    }
/*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
