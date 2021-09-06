using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> vertexsTransform;

    public List<Vector3> vertexs;

    private void Awake()
    {
        for (int i = 0; i < vertexsTransform.Count; i++)
        {
            vertexs.Add(vertexsTransform[i].position);
        }
    }

    public Vector3 getClose(Vector3 colPos)
    {
        float min = 100000f;
        Vector3 res = new Vector3();
        for (int i = 0; i < vertexs.Count; i++)
        {
            if ((colPos - vertexs[i]).magnitude < min)
            {
                min = (colPos - vertexs[i]).magnitude;
                res = vertexs[i];
            }
        }
        return res;
    }
}