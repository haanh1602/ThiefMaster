using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmLine : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform attachPosition;

    [SerializeField]
    private List<Vector3> pivotPoints = new List<Vector3>();

    private LineRenderer lineRenderer;
    private PolygonCollider2D myCollider;

    private Color successColor = Color.cyan;
    private Color normalColor = Color.black;

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
        myCollider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        
    }

    void Update()
    {
        pivotPoints[pivotPoints.Count - 1] = attachPosition.position;

        for (int i = 0; i < pivotPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, pivotPoints[i]);
        }
        updateCollider();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Barrier"))
        {
            Vector3 collisionPosition = collision.gameObject.GetComponent<Collider2D>().ClosestPoint(transform.position);
            //Debug.Log(collisionPosition);
            pivotPoints.Insert(pivotPoints.Count, collisionPosition);
            lineRenderer.positionCount++;
            myCollider.pathCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Vector3 collisionPosition = collision.GetContact(collision.contactCount - 1).point;
            //Debug.Log(collisionPosition);
            pivotPoints.Insert(pivotPoints.Count, collisionPosition);
            lineRenderer.positionCount++;
            myCollider.pathCount++;
        }
    }

    private void updateCollider()
    {
        var points = new List<Vector2>();
        int startPointIndex = 0;
        if(myCollider.pathCount > 1)
        {
            startPointIndex = pivotPoints.Count - 2;
        }
        for(int i = startPointIndex; i < pivotPoints.Count; i++)
        {
            var pos = myCollider.transform.InverseTransformPoint(pivotPoints[i]);
            points.AddRange(calculatePoints(pos, pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], i % 2));
        }
        myCollider.SetPath(myCollider.pathCount - 1, points.ToArray());

    }

    private List<Vector2> calculatePoints(Vector2 pos, Vector2 startPoint, Vector2 endPoint, int odd)
    {
        float value = (odd==1)? -1f : 1f;
        float w = lineRenderer.endWidth;
        float m = (endPoint.y - startPoint.y) / (endPoint.x - startPoint.x);
        float deltaX = (w / 2f) * (m / (Mathf.Sqrt(m * m + 1)));
        float deltaY = (w / 2f) * (1 / (Mathf.Sqrt(m * m + 1)));
        return new List<Vector2>() { pos + new Vector2(deltaX*value, deltaY*(-value)), pos + new Vector2(deltaX*(-value), deltaY*value)};
    }
}
