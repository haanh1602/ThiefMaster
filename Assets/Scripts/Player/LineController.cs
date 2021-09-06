using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform attachPosition;
    [SerializeField]
    private GameObject player;

    public List<Vector3> pivotPoints = new List<Vector3>();

    public LineRenderer lineRenderer;

    //public GameObject checkPoint;

    private Color successColor = Color.cyan;
    private Color normalColor = Color.black;

    private Vector3 direction = new Vector3();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = normalColor;
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.sortingOrder = 1;
        lineRenderer.SetPosition(0, startPoint.position);
        pivotPoints.Add(startPoint.position);
        lineRenderer.SetPosition(1, attachPosition.position);
        pivotPoints.Add(attachPosition.position);
    }

    private void Start()
    {
        //myCollider.attachedRigidbody.gravityScale = 0;
    }

    void Update()
    {
        direction = attachPosition.position - pivotPoints[pivotPoints.Count - 1];
        pivotPoints[pivotPoints.Count - 1] = attachPosition.position;
        if(!player.gameObject.GetComponent<Player>().Win)
        {
            if (pivotPoints.Count > 2)
            {
                if (RemovePrePoint(pivotPoints[pivotPoints.Count - 1]))
                {
                    pivotPoints.RemoveAt(pivotPoints.Count - 2);
                    lineRenderer.positionCount--;
                }
            }
        }
        for (int i = 0; i < pivotPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, pivotPoints[i]);
        }
    }

    private void FixedUpdate()
    {
        if(!player.gameObject.GetComponent<Player>().Win)
        {
            RaycastHit2D[] hit1 = Physics2D.RaycastAll(pivotPoints[pivotPoints.Count - 1], pivotPoints[pivotPoints.Count - 2] - pivotPoints[pivotPoints.Count - 1], Vector3.Distance(pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1]));
            bool barrierCheck = true;
            for (int i = 0; i < hit1.Length; i++)
            {
                if (barrierCheck && hit1[i].collider.gameObject.CompareTag("Barrier"))
                {
                    Vector3 addPoint = hit1[i].collider.gameObject.GetComponent<BarrierController>().GetClose(hit1[i].point);
                    if (addPoint != pivotPoints[pivotPoints.Count - 2])
                    {
                        pivotPoints.Insert(pivotPoints.Count - 1, addPoint);
                        lineRenderer.positionCount++;
                        //Debug.Log("Add:" + hit1[i].collider.gameObject.GetComponent<BarrierController>().getClose(hit1[i].point));
                        barrierCheck = false;
                    }
                }
                if (hit1[i].collider.gameObject.CompareTag("Objects"))
                {
                    player.GetComponent<Player>().Failed = true;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void UpdateCollider1()
    {

    }

    private List<Vector3> GetPointsBetweenTwoVectors(Vector3 v1, Vector3 v2)
    {
        List<Vector3> res = new List<Vector3>();
        float a, b, c;
        float space = lineRenderer.endWidth;
        b = v2.x - v1.x;
        a = -(v2.y - v1.y);
        c = -(v2.x * a + v1.y * b);
        Debug.Log(v2 + "\n" + v1 + "\n" + a + "x + " + b + "y + " + c + " = 0");
        int n = Mathf.Min(Mathf.Abs((int)((v2.x - v1.x) / space)), Mathf.Abs((int)((v2.x - v1.x) / space)));
        float _x = v2.x - v1.x;
        _x = _x / Mathf.Abs(_x);
        float _y = v2.y - v1.y;
        _y = _y / Mathf.Abs(_y);
        for (int i = 0; i <= Mathf.Abs(n); i++)
        {
            res.Add(new Vector3(v2.x - i * space * _x, (-c - a * (v2.x - i * space * _x) + space * _y) / b, v2.z));
        }
        return res;
    }

    public bool RemovePrePoint(Vector3 point)
    {
        if(Vector3.Distance(point, pivotPoints[pivotPoints.Count - 2]) > lineRenderer.endWidth)
        {
            RaycastHit2D hit2 = Physics2D.Raycast(pivotPoints[pivotPoints.Count - 3], point - pivotPoints[pivotPoints.Count - 3], Vector3.Distance(point, pivotPoints[pivotPoints.Count - 3]));
            if (!hit2)
            {
                return RemovePrePoint((point + pivotPoints[pivotPoints.Count - 2]) / 2);
            } else
            {
                if (hit2.collider.gameObject.tag == "Barrier")
                {
                    return false;
                } else
                {
                    //Debug.Log((Vector3)((point + pivotPoints[pivotPoints.Count - 2]) / 2));
                    return RemovePrePoint((point + pivotPoints[pivotPoints.Count - 2]) / 2);
                }
            } 
        } else {
            return true;
        }
    }
}
