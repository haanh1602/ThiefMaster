using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform attachPosition;
    private Vector3 prePos = new Vector3();
    [SerializeField]
    private GameObject player;

    public List<Vector3> pivotPoints = new List<Vector3>();

    public LineRenderer lineRenderer;

    private Color successColor = Color.green;
    private Color normalColor = Color.black;

    private Vector3 direction = new Vector3();

    [SerializeField]
    public GameObject checkPoint;
    public List<Vector3> points = new List<Vector3>();

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
        prePos = pivotPoints[pivotPoints.Count - 1];
    }

    private void Start()
    {

    }

    void Update()
    {
        prePos = pivotPoints[pivotPoints.Count - 1];
        direction = attachPosition.position - pivotPoints[pivotPoints.Count - 1];
        pivotPoints[pivotPoints.Count - 1] = attachPosition.position;
        if (!player.gameObject.GetComponent<Player>().Win && !player.gameObject.GetComponent<Player>().Failed && prePos != pivotPoints[pivotPoints.Count - 1])
        {
            AddPivotPoints(pivotPoints[pivotPoints.Count - 1], prePos);
        }
        if (!player.gameObject.GetComponent<Player>().Win && !player.gameObject.GetComponent<Player>().Failed)
        {
            if (pivotPoints.Count > 2)
            {
                for (int i = 1; i < pivotPoints.Count; i++)
                {
                    if (Normalize(i))
                    {
                        pivotPoints.RemoveAt(i - 1);
                        lineRenderer.positionCount--;
                        i--;
                    }
                }
            }

            for (int i = 1; i < pivotPoints.Count; i++)
            {
                CheckCollisionWithObjects(i);
            }
        }
        for (int i = 0; i < pivotPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, pivotPoints[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void UpdateCollider1()
    {

    }

    public bool RemovePrePoint(Vector3 point)
    {
        if (Vector3.Distance(point, pivotPoints[pivotPoints.Count - 2]) > lineRenderer.endWidth)
        {
            RaycastHit2D hit2 = Physics2D.Raycast(pivotPoints[pivotPoints.Count - 3], point - pivotPoints[pivotPoints.Count - 3], Vector3.Distance(point, pivotPoints[pivotPoints.Count - 3]));
            if (!hit2)
            {
                return RemovePrePoint((point + pivotPoints[pivotPoints.Count - 2]) / 2);
            }
            else
            {
                if (hit2.collider.gameObject.tag == "Barrier")
                {
                    return false;
                }
                else
                {
                    //Debug.Log((Vector3)((point + pivotPoints[pivotPoints.Count - 2]) / 2));
                    return RemovePrePoint((point + pivotPoints[pivotPoints.Count - 2]) / 2);
                }
            }
        }
        else
        {
            return true;
        }
    }

    public bool CheckCollisionWithObjects(int index)
    {
        if (index < 1) return false;
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(pivotPoints[index - 1], pivotPoints[index] - pivotPoints[index - 1], Vector3.Distance(pivotPoints[index - 1], pivotPoints[index]));
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Objects"))
                {
                    player.GetComponent<Player>().Failed = true;
                    return true;
                }
            }
        }
        return false;
    }

    public bool Normalize(int index)
    {
        if (index < 2) return false;
        else
        {
            List<Vector3> points = GetPointsFrom(pivotPoints[index - 1], pivotPoints[index], lineRenderer.endWidth);
            for (int i = 0; i < points.Count; i++)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(pivotPoints[index - 2], points[i] - pivotPoints[index - 2], Vector3.Distance(points[i], pivotPoints[index - 2]));
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag("Barrier"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }


    List<Vector3> GetPointsFrom(Vector3 start, Vector3 end, float distance)
    {
        List<Vector3> res = new List<Vector3>();
        float maxDistance = Vector3.Distance(start, end);
        Vector3 direct = end - start;
        int i = 0;
        while (i * distance <= maxDistance)
        {
            res.Add(start + direct * (i * distance) / maxDistance);
            i++;
        }
        if (i * distance > maxDistance)
        {
            res.Add(end);
        }
        //Debug.Log(res[0] + ", " + pivotPoints[pivotPoints.Count - 2]);
        return res;
    }

    List<Vector3> GetPointsBetween(Vector3 start, Vector3 end, float distance)
    {
        List<Vector3> res = new List<Vector3>();
        float maxDistance = Vector3.Distance(start, end);
        Vector3 direct = end - start;
        int i = 1;
        while (i * distance < maxDistance)
        {
            res.Add(start + direct * (i * distance) / maxDistance);
            i++;
        }
        return res;
    }

    public void AddPivotPoints(Vector3 start, Vector3 end)
    {
        Vector3 pivot2ToLast = pivotPoints[pivotPoints.Count - 2];
        List<Vector3> addPoints = new List<Vector3>();
        RaycastHit2D fromStartToEnd = Physics2D.Raycast(start, end - start, Vector3.Distance(start, end));
        while (fromStartToEnd && fromStartToEnd.collider.gameObject.CompareTag("Barrier"))
        {
            end += end - pivot2ToLast;
            fromStartToEnd = Physics2D.Raycast(start, end - start, Vector3.Distance(start, end));
        };
        if (!player.gameObject.GetComponent<Player>().Win)
        {
            points = GetPointsFrom(start, end, lineRenderer.endWidth / 20f);
            for (int i = 0; i < points.Count; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(pivot2ToLast, points[i] - pivot2ToLast, Vector3.Distance(points[i], pivot2ToLast));
                if (hit)
                {
                    if (hit.collider.gameObject.CompareTag("Barrier"))
                    {
                        Vector3 addPoint = hit.collider.GetComponent<BarrierController>().GetClose(hit.point);
                        if (!addPoints.Contains(addPoint) && addPoint != pivot2ToLast)
                        {
                            addPoints.Add(addPoint);
                        }
                    }
                    else if (!hit.collider.gameObject.CompareTag("Hand"))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            for (int i = points.Count - 1; i >= 0; i--)
            {
                RaycastHit2D hit = Physics2D.Raycast(points[i], pivot2ToLast - points[i], Vector3.Distance(pivot2ToLast, points[i]));
                if (hit)
                {
                    if (hit.collider.gameObject.CompareTag("Barrier"))
                    {
                        Vector3 addPoint = hit.collider.GetComponent<BarrierController>().GetClose(hit.point);
                        if (!addPoints.Contains(addPoint) && addPoint != pivot2ToLast)
                        {
                            addPoints.Add(addPoint);
                        }
                    }
                }
            }
            pivotPoints.InsertRange(pivotPoints.Count - 1, addPoints);
            lineRenderer.positionCount += addPoints.Count;
        }
    }
}
