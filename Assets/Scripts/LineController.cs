/*using System.Collections;
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

    private LineRenderer lineRenderer;
    private PolygonCollider2D myCollider;

    public GameObject checkPoint;

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
        *//*        myRigid = GetComponent<Rigidbody2D>();*//*
        //myCollider = lineRenderer.gameObject.AddComponent<BoxCollider2D>();
        //myCollider.isTrigger = true;
    }

    private void Start()
    {
        //myCollider.attachedRigidbody.gravityScale = 0;
    }

    void Update()
    {
        pivotPoints[pivotPoints.Count - 1] = attachPosition.position;

        for (int i = 0; i < pivotPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, pivotPoints[i]);
        }

        *//*        for (int i = pivotPoints.Count - 2; i < pivotPoints.Count; i++)
                {
                    lineRenderers[lineRenderers.Count - 1].SetPosition(lineRenderers[lineRenderers.Count - 1].positionCount, pivotPoints[i]);
                }*//*
        updateCollider();
    }

*//*    private void FixedUpdate()
    {
        RaycastHit2D hit;
        List<Vector2> points = getPointBetweenTwoVectors(pivotPoints[pivotPoints.Count - 1], pivotPoints[pivotPoints.Count - 2]);
        for (int i = 0; i < points.Count; i++)
        {
            hit = Physics2D.Raycast(points[i], transform.TransformDirection(Vector2.left), LayerMask.NameToLayer("Barrier"), 1, 0.05f);
            Debug.Log(hit.point);
        }
    }*/

/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.contacts[0].point);
        checkPoint.transform.position = collision.contacts[0].point;
    }*/

/*    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
 *//*           Vector3 colPos = myCollider.bounds.ClosestPoint(collision.transform.position);
            collision.OverlapCollider(1, myCollider);*/
/*Vector3 colPos = collision.bounds.ClosestPoint(pivotPoints[pivotPoints.Count - 1]);
Debug.Log(pivotPoints[pivotPoints.Count - 1] - pivotPoints[pivotPoints.Count - 2] + "\n" + colPos);*//*
//checkPoint.transform.position = colPos;
checkPoint.transform.position = getCollisionPoint(collision);

//this is the Vector3 position of the point of contact
*//*            Vector3 collisionPosition = collision.gameObject.GetComponent<Collider2D>().ClosestPoint(transform.position);
            Debug.Log(collisionPosition);
            pivotPoints.Insert(pivotPoints.Count - 1, collisionPosition);
            lineRenderer.positionCount++;
            myCollider.pathCount++;*//*
}
}*//*

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Barrier"))
        {
            Vector3 colPos = getCollisionPoint(collision);
            checkPoint.transform.position = colPos;
            pivotPoints.Insert(pivotPoints.Count - 1, colPos);
            lineRenderer.positionCount++;
            myCollider.pathCount++;
            //checkPoint.transform.position = getCollisionPoint1(pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], col);
        }
        if (collision.gameObject.CompareTag("Objects"))
        {
            player.GetComponent<Player>().Failed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Vector3 colPos = collision.GetContact(collision.contactCount - 1).point;
            checkPoint.transform.position = colPos;
            pivotPoints.Insert(pivotPoints.Count - 1, colPos);
            lineRenderer.positionCount++;
            myCollider.pathCount++;
            //checkPoint.transform.position = getCollisionPoint1(pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], col);
        }
    }


    *//*    private void updateCollider()
        {
            var points = new List<Vector2>();
            int startPointIndex = 0;
            if (myCollider.pathCount > 1)
            {
                startPointIndex = pivotPoints.Count - 2;
            }
            for (int i = startPointIndex; i < pivotPoints.Count; i++)
            {
                //var pos = myCollider.transform.InverseTransformPoint(pivotPoints[i]);
                points.Add(pivotPoints[i]);
                //points.AddRange(calculatePoints(pos, pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], i % 2));
            }
            *//*        pivotPoints.ForEach(x, i =>
                    {

                        //points.Add(new Vector2(pos.x - lineRenderer.endWidth * Mathf.Cos(Vector2.Angle(new Vector2(1, 0), pos) / (2 * Mathf.PI)) / 2, pos.y + lineRenderer.endWidth * Mathf.Sin(Vector2.Angle(new Vector2(1, 0), pos) / (2 * Mathf.PI)) / 2));
                        //points.Add(new Vector2(pos.x + lineRenderer.endWidth * Mathf.Cos(Vector2.Angle(new Vector2(1, 0), pos) / (2 * Mathf.PI)) / 2, pos.y - lineRenderer.endWidth * Mathf.Sin(Vector2.Angle(new Vector2(1, 0), pos) / (2 * Mathf.PI)) / 2));
                    });*//*
            //myCollider.points = points.ToArray();
            myCollider.SetPath(myCollider.pathCount - 1, points.ToArray());
        }*//*

    private void updateCollider()
    {
        var points = new List<Vector2>();
        int startPointIndex = 0;
        if (myCollider.pathCount > 1)
        {
            startPointIndex = pivotPoints.Count - 3;
        }
        for (int i = startPointIndex; i < pivotPoints.Count; i++)
        {
            var pos = myCollider.transform.InverseTransformPoint(pivotPoints[i]);
            points.AddRange(calculatePoints(pos, pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], i % 2));
            if(*//*myCollider.pathCount > 1 && *//*points.Count == 4)
            {
                myCollider.SetPath((i - 1), points.ToArray());
                points = points.GetRange(2, 2);
            }
        }
        //myCollider.SetPath(myCollider.pathCount - 1, points.ToArray());
        // Just two lasts
*//*        for (int i = pivotPoints.Count - 3; i < pivotPoints.Count; i++)
        {
            var points = new List<Vector2>();
            var pos = myCollider.transform.InverseTransformPoint(pivotPoints[i]);
            if (i != pivotPoints.Count - 1)
            {
                points.AddRange(calculatePoints(pos, pivotPoints[i], pivotPoints[i + 1], i % 2));
                myCollider.SetPath(myCollider.pathCount - 2, points.ToArray());
            }
            else
            {
                points.AddRange(calculatePoints(pos, pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], i % 2));
                myCollider.SetPath(myCollider.pathCount - 1, points.ToArray());
            }
        }*//*
    }


    private void updateCollider1()
    {

    }

        private List<Vector2> calculatePoints(Vector2 pos, Vector2 startPoint, Vector2 endPoint, int odd)
    {
        float value = (odd == 1) ? -1f : 1f;
        float w = lineRenderer.endWidth;
        float m = (endPoint.y - startPoint.y) / (endPoint.x - startPoint.x);
        float deltaX = (w / 2f) * (m / (Mathf.Sqrt(m * m + 1)));
        float deltaY = (w / 2f) * (1 / (Mathf.Sqrt(m * m + 1)));
        return new List<Vector2>() { pos + new Vector2(deltaX * value, deltaY * (-value)), pos + new Vector2(deltaX * (-value), deltaY * value) };
    }

    private Vector3 getCollisionPoint(Collider2D collision)
    {
        float a, b, c;
        float space = lineRenderer.endWidth;
        b = pivotPoints[pivotPoints.Count - 1].x - pivotPoints[pivotPoints.Count - 2].x;
        a = -(pivotPoints[pivotPoints.Count - 1].y - pivotPoints[pivotPoints.Count - 2].y);
        c = -(pivotPoints[pivotPoints.Count - 1].x * a + pivotPoints[pivotPoints.Count - 1].y * b);
        Debug.Log(pivotPoints[pivotPoints.Count - 1] + "\n" + pivotPoints[pivotPoints.Count - 2] + "\n" + a + "x + " + b + "y + " + c + " = 0");
        int n = (int)((pivotPoints[pivotPoints.Count - 1].x - pivotPoints[pivotPoints.Count - 2].x) / space);
        float _x = pivotPoints[pivotPoints.Count - 1].x - pivotPoints[pivotPoints.Count - 2].x;
        _x = _x / Mathf.Abs(_x);
        float _y = pivotPoints[pivotPoints.Count - 1].y - pivotPoints[pivotPoints.Count - 2].y;
        _y = _y / Mathf.Abs(_y);

        *//*        Vector3 line = pivotPoints[pivotPoints.Count - 1] - pivotPoints[pivotPoints.Count - 2];
                line = new Vector3(line.x + pivotPoints[pivotPoints.Count - 2].x, line.y + pivotPoints[pivotPoints.Count - 2].y, 0);
                Debug.Log(line);
                int n = (int)(line.magnitude / 0.5f);*//*
        float min = 1000000f;
        Vector3 res = new Vector3();
        for (int i = 0; i <= Mathf.Abs(n); i++)
        {
            Vector3 checkPoint = new Vector3(pivotPoints[pivotPoints.Count - 1].x - i * space * _x,
                (-c - a * (pivotPoints[pivotPoints.Count - 1].x - i * space * _x) + space * _y) / b, 0);
            Vector3 colPos = collision.bounds.ClosestPoint(checkPoint);
            min = Mathf.Min(min, (colPos - checkPoint).magnitude);
            if (min == (colPos - checkPoint).magnitude)
            {
                res = checkPoint;
            }
            if (collision.OverlapPoint(checkPoint))
            {
                break;
            }
        }
        return res;
    }

    private List<Vector2> getPointBetweenTwoVectors(Vector2 v1, Vector2 v2)
    {
        List<Vector2> res = new List<Vector2>();
        float a, b, c;
        float space = lineRenderer.endWidth;
        b = v2.x - v1.x;
        a = -(v2.y - v1.y);
        c = -(v2.x * a + v1.y * b);
        Debug.Log(v2 + "\n" + v1 + "\n" + a + "x + " + b + "y + " + c + " = 0");
        int n = (int)((v2.x - v1.x) / space);
        float _x = v2.x - v1.x;
        _x = _x / Mathf.Abs(_x);
        float _y = v2.y - v1.y;
        _y = _y / Mathf.Abs(_y);
        for (int i = 0; i <= Mathf.Abs(n); i++)
        {
            res.Add(new Vector2(v2.x - i * space * _x, (-c - a * (v2.x - i * space * _x) + space * _y) / b));
        }
        return res;
    }

*//*
    private List<Vector2> linePoints ()
    {
        // linear ax + by + c = 0
        float a, b, c;
        a = pivotPoints[pivotPoints.Count - 1].x - pivotPoints[pivotPoints.Count - 2].x;
        b = -(pivotPoints[pivotPoints.Count - 1].y - pivotPoints[pivotPoints.Count - 2].y);
        c = -(pivotPoints[pivotPoints.Count - 1].x + pivotPoints[pivotPoints.Count - 1].y);
        Vector3 line = (pivotPoints[pivotPoints.Count - 1] + pivotPoints[pivotPoints.Count - 2]) / 2;
        Debug.Log(line);
        int n = (int)(line.magnitude / 0.5f);
        float min = 1000000f;
    }*/

/*    private Vector3 getCollisionPoint1(Vector3 left, Vector3 right, Collider2D col)
    {
        Vector3 mid = (left + right) / 2;
        if ((col.bounds.ClosestPoint(mid) - mid).magnitude < 0.05f) return mid;
        float leftDis = (col.bounds.ClosestPoint(left) - left).magnitude;
        float rightDis = (col.bounds.ClosestPoint(right) - right).magnitude;
        if (leftDis < rightDis) return getCollisionPoint1(left, mid, col);
        else return getCollisionPoint1(right, mid, col);
    }*//*
}
*/

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
    //private PolygonCollider2D myCollider;

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
        //myCollider = GetComponent<PolygonCollider2D>();
        /*        myRigid = GetComponent<Rigidbody2D>();*/
        //myCollider = lineRenderer.gameObject.AddComponent<BoxCollider2D>();
        //myCollider.isTrigger = true;
    }

    private void Start()
    {
        //myCollider.attachedRigidbody.gravityScale = 0;
    }

    void Update()
    {
        direction = attachPosition.position - pivotPoints[pivotPoints.Count - 1];
        pivotPoints[pivotPoints.Count - 1] = attachPosition.position;
        /*        if (hit1.collider != null)
                {
                    if(!hit1.collider.gameObject.CompareTag("Hand"))
                    {
                        Debug.Log("Raied");
                    }
                    if (hit1.collider.gameObject.CompareTag("Barrier"))
                    {
                        pivotPoints.Insert(pivotPoints.Count - 1, hit1.collider.gameObject.GetComponent<BarrierController>().getClose(hit1.point));
                        lineRenderer.positionCount++;
                        Debug.Log(hit1.point);
                    }
                }*/
        if(!player.gameObject.GetComponent<Player>().Win)
        {
            if (pivotPoints.Count > 2)
            {
                if (removePrePoint(pivotPoints[pivotPoints.Count - 1]))
                {
                    pivotPoints.RemoveAt(pivotPoints.Count - 2);
                    lineRenderer.positionCount--;
                }
                /*RaycastHit2D hit2 = Physics2D.Raycast(pivotPoints[pivotPoints.Count - 3], pivotPoints[pivotPoints.Count - 1] - pivotPoints[pivotPoints.Count - 3], Vector3.Distance(pivotPoints[pivotPoints.Count - 1], pivotPoints[pivotPoints.Count - 3]));
                if (!hit2)
                {
                    pivotPoints.RemoveAt(pivotPoints.Count - 2);
                    lineRenderer.positionCount--;
                }
                if (hit2.collider)
                {
                    if (hit2.collider.gameObject.tag != "Barrier")
                    {
                        pivotPoints.RemoveAt(pivotPoints.Count - 2);
                        lineRenderer.positionCount--;
                        Debug.Log("Remove" + hit2.point);
                    }
                }*/
                /*
                bool remove = true;
                List<Vector3> rayList = getPointBetweenTwoVectors(pivotPoints[pivotPoints.Count - 1], pivotPoints[pivotPoints.Count - 2]);
                Debug.Log(rayList.Count);
                for (int i = 0; i < rayList.Count; i++)
                {
                    RaycastHit2D hit2 = Physics2D.Raycast(pivotPoints[pivotPoints.Count - 3], rayList[i] - pivotPoints[pivotPoints.Count - 3], Vector3.Distance(rayList[i], pivotPoints[pivotPoints.Count - 3]));
    *//*                if (!hit2)
                    {
                        pivotPoints.RemoveAt(pivotPoints.Count - 2);
                        lineRenderer.positionCount--;
                    }*//*
                    if (hit2.collider)
                    {
                        if (hit2.collider.gameObject.tag == "Barrier")
    *//*                    {
                            pivotPoints.RemoveAt(pivotPoints.Count - 2);
                            lineRenderer.positionCount--;
                            Debug.Log("Remove" + hit2.point);
                        } else*//*
                        {
                            remove = false;
                            break;
                        }
                    }
                }
                if(remove)
                {
                    Debug.Log("Remove: " + pivotPoints[pivotPoints.Count - 2]);
                    pivotPoints.RemoveAt(pivotPoints.Count - 2);
                    lineRenderer.positionCount--;
                }*/
            }
        }
/*
        RaycastHit2D[] hit1 = Physics2D.RaycastAll(pivotPoints[pivotPoints.Count - 1], pivotPoints[pivotPoints.Count - 2] - pivotPoints[pivotPoints.Count - 1], Vector3.Distance(pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1]));
        bool barrierCheck = true;
        for (int i = 0; i < hit1.Length; i++)
        {
            if (barrierCheck && hit1[i].collider.gameObject.CompareTag("Barrier"))
            {
                Vector3 addPoint = hit1[i].collider.gameObject.GetComponent<BarrierController>().getClose(hit1[i].point);
                if (addPoint != pivotPoints[pivotPoints.Count - 2])
                {
                    pivotPoints.Insert(pivotPoints.Count - 1, addPoint);
                    lineRenderer.positionCount++;
                    Debug.Log("Add:" + hit1[i].collider.gameObject.GetComponent<BarrierController>().getClose(hit1[i].point));
                    barrierCheck = false;
                }
            }
            if (hit1[i].collider.gameObject.CompareTag("Objects"))
            {
                player.GetComponent<Player>().Failed = true;
                break;
            }
        }*/


        for (int i = 0; i < pivotPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, pivotPoints[i]);
        }

        /*        for (int i = pivotPoints.Count - 2; i < pivotPoints.Count; i++)
                {
                    lineRenderers[lineRenderers.Count - 1].SetPosition(lineRenderers[lineRenderers.Count - 1].positionCount, pivotPoints[i]);
                }*/
        //updateCollider();
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
                    Vector3 addPoint = hit1[i].collider.gameObject.GetComponent<BarrierController>().getClose(hit1[i].point);
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

    /*    private void FixedUpdate()
        {
            RaycastHit2D hit;
            List<Vector2> points = getPointBetweenTwoVectors(pivotPoints[pivotPoints.Count - 1], pivotPoints[pivotPoints.Count - 2]);
            for (int i = 0; i < points.Count; i++)
            {
                hit = Physics2D.Raycast(points[i], transform.TransformDirection(Vector2.left), LayerMask.NameToLayer("Barrier"), 1, 0.05f);
                Debug.Log(hit.point);
            }
        }*/

    /*    private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.contacts[0].point);
            checkPoint.transform.position = collision.contacts[0].point;
        }*/

    /*    private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Barrier"))
            {
     *//*           Vector3 colPos = myCollider.bounds.ClosestPoint(collision.transform.position);
                collision.OverlapCollider(1, myCollider);*/
    /*Vector3 colPos = collision.bounds.ClosestPoint(pivotPoints[pivotPoints.Count - 1]);
    Debug.Log(pivotPoints[pivotPoints.Count - 1] - pivotPoints[pivotPoints.Count - 2] + "\n" + colPos);*//*
    //checkPoint.transform.position = colPos;
    checkPoint.transform.position = getCollisionPoint(collision);

    //this is the Vector3 position of the point of contact
    *//*            Vector3 collisionPosition = collision.gameObject.GetComponent<Collider2D>().ClosestPoint(transform.position);
                Debug.Log(collisionPosition);
                pivotPoints.Insert(pivotPoints.Count - 1, collisionPosition);
                lineRenderer.positionCount++;
                myCollider.pathCount++;*//*
}
}*/

    /*    private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Barrier"))
            {
                Vector3 colPos = getCollisionPoint(collision);
                checkPoint.transform.position = colPos;
                pivotPoints.Insert(pivotPoints.Count - 1, colPos);
                lineRenderer.positionCount++;
                myCollider.pathCount++;
                //checkPoint.transform.position = getCollisionPoint1(pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], col);
            }
            if (collision.gameObject.CompareTag("Objects"))
            {
                player.GetComponent<Player>().Failed = true;
            }
        }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    /*    private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Barrier"))
            {
                Vector3 colPos = collision.gameObject.GetComponent<BarrierController>().getClose(collision.GetContact(collision.contactCount - 1).point);
                    //collision.GetContact(collision.contactCount - 1).point;
                checkPoint.transform.position = colPos;
                pivotPoints.Insert(pivotPoints.Count - 1, colPos);
                lineRenderer.positionCount++;
                myCollider.pathCount++;
                //checkPoint.transform.position = getCollisionPoint1(pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], col);
            }
        }*/


    /*    private void updateCollider()
        {
            var points = new List<Vector2>();
            int startPointIndex = 0;
            if (myCollider.pathCount > 1)
            {
                startPointIndex = pivotPoints.Count - 2;
            }
            for (int i = startPointIndex; i < pivotPoints.Count; i++)
            {
                //var pos = myCollider.transform.InverseTransformPoint(pivotPoints[i]);
                points.Add(pivotPoints[i]);
                //points.AddRange(calculatePoints(pos, pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], i % 2));
            }
            *//*        pivotPoints.ForEach(x, i =>
                    {

                        //points.Add(new Vector2(pos.x - lineRenderer.endWidth * Mathf.Cos(Vector2.Angle(new Vector2(1, 0), pos) / (2 * Mathf.PI)) / 2, pos.y + lineRenderer.endWidth * Mathf.Sin(Vector2.Angle(new Vector2(1, 0), pos) / (2 * Mathf.PI)) / 2));
                        //points.Add(new Vector2(pos.x + lineRenderer.endWidth * Mathf.Cos(Vector2.Angle(new Vector2(1, 0), pos) / (2 * Mathf.PI)) / 2, pos.y - lineRenderer.endWidth * Mathf.Sin(Vector2.Angle(new Vector2(1, 0), pos) / (2 * Mathf.PI)) / 2));
                    });*//*
            //myCollider.points = points.ToArray();
            myCollider.SetPath(myCollider.pathCount - 1, points.ToArray());
        }*/

    private void updateCollider()
    {
        /*        var points = new List<Vector2>();
                int startPointIndex = 0;
                if (myCollider.pathCount > 1)
                {
                    startPointIndex = pivotPoints.Count - 3;
                }
                for (int i = startPointIndex; i < pivotPoints.Count; i++)
                {
                    var pos = myCollider.transform.InverseTransformPoint(pivotPoints[i]);
                    points.AddRange(calculatePoints(pos, pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], i % 2));
                    if(*//*myCollider.pathCount > 1 && *//*points.Count == 4)
                    {
                        myCollider.SetPath((i - 1), points.ToArray());
                        points = points.GetRange(2, 2);
                    }
                }*/
        //myCollider.SetPath(myCollider.pathCount - 1, points.ToArray());
        // Just two lasts
        /*        for (int i = pivotPoints.Count - 3; i < pivotPoints.Count; i++)
                {
                    var points = new List<Vector2>();
                    var pos = myCollider.transform.InverseTransformPoint(pivotPoints[i]);
                    if (i != pivotPoints.Count - 1)
                    {
                        points.AddRange(calculatePoints(pos, pivotPoints[i], pivotPoints[i + 1], i % 2));
                        myCollider.SetPath(myCollider.pathCount - 2, points.ToArray());
                    }
                    else
                    {
                        points.AddRange(calculatePoints(pos, pivotPoints[pivotPoints.Count - 2], pivotPoints[pivotPoints.Count - 1], i % 2));
                        myCollider.SetPath(myCollider.pathCount - 1, points.ToArray());
                    }
                }*/
    }


    private void updateCollider1()
    {

    }

    private List<Vector2> calculatePoints(Vector2 pos, Vector2 startPoint, Vector2 endPoint, int odd)
    {
        float value = (odd == 1) ? -1f : 1f;
        float w = lineRenderer.endWidth;
        float m = (endPoint.y - startPoint.y) / (endPoint.x - startPoint.x);
        float deltaX = (w / 2f) * (m / (Mathf.Sqrt(m * m + 1)));
        float deltaY = (w / 2f) * (1 / (Mathf.Sqrt(m * m + 1)));
        return new List<Vector2>() { pos + new Vector2(deltaX * value, deltaY * (-value)), pos + new Vector2(deltaX * (-value), deltaY * value) };
    }

    private Vector3 getCollisionPoint(Collider2D collision)
    {
        float a, b, c;
        float space = lineRenderer.endWidth;
        b = pivotPoints[pivotPoints.Count - 1].x - pivotPoints[pivotPoints.Count - 2].x;
        a = -(pivotPoints[pivotPoints.Count - 1].y - pivotPoints[pivotPoints.Count - 2].y);
        c = -(pivotPoints[pivotPoints.Count - 1].x * a + pivotPoints[pivotPoints.Count - 1].y * b);
        Debug.Log(pivotPoints[pivotPoints.Count - 1] + "\n" + pivotPoints[pivotPoints.Count - 2] + "\n" + a + "x + " + b + "y + " + c + " = 0");
        int n = (int)((pivotPoints[pivotPoints.Count - 1].x - pivotPoints[pivotPoints.Count - 2].x) / space);
        float _x = pivotPoints[pivotPoints.Count - 1].x - pivotPoints[pivotPoints.Count - 2].x;
        _x = _x / Mathf.Abs(_x);
        float _y = pivotPoints[pivotPoints.Count - 1].y - pivotPoints[pivotPoints.Count - 2].y;
        _y = _y / Mathf.Abs(_y);

        /*        Vector3 line = pivotPoints[pivotPoints.Count - 1] - pivotPoints[pivotPoints.Count - 2];
                line = new Vector3(line.x + pivotPoints[pivotPoints.Count - 2].x, line.y + pivotPoints[pivotPoints.Count - 2].y, 0);
                Debug.Log(line);
                int n = (int)(line.magnitude / 0.5f);*/
        float min = 1000000f;
        Vector3 res = new Vector3();
        for (int i = 0; i <= Mathf.Abs(n); i++)
        {
            Vector3 checkPoint = new Vector3(pivotPoints[pivotPoints.Count - 1].x - i * space * _x,
                (-c - a * (pivotPoints[pivotPoints.Count - 1].x - i * space * _x) + space * _y) / b, 0);
            Vector3 colPos = collision.bounds.ClosestPoint(checkPoint);
            min = Mathf.Min(min, (colPos - checkPoint).magnitude);
            if (min == (colPos - checkPoint).magnitude)
            {
                res = checkPoint;
            }
            if (collision.OverlapPoint(checkPoint))
            {
                break;
            }
        }
        return res;
    }

    private List<Vector3> getPointBetweenTwoVectors(Vector3 v1, Vector3 v2)
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

    /*
        private List<Vector2> linePoints ()
        {
            // linear ax + by + c = 0
            float a, b, c;
            a = pivotPoints[pivotPoints.Count - 1].x - pivotPoints[pivotPoints.Count - 2].x;
            b = -(pivotPoints[pivotPoints.Count - 1].y - pivotPoints[pivotPoints.Count - 2].y);
            c = -(pivotPoints[pivotPoints.Count - 1].x + pivotPoints[pivotPoints.Count - 1].y);
            Vector3 line = (pivotPoints[pivotPoints.Count - 1] + pivotPoints[pivotPoints.Count - 2]) / 2;
            Debug.Log(line);
            int n = (int)(line.magnitude / 0.5f);
            float min = 1000000f;
        }*/

    /*    private Vector3 getCollisionPoint1(Vector3 left, Vector3 right, Collider2D col)
        {
            Vector3 mid = (left + right) / 2;
            if ((col.bounds.ClosestPoint(mid) - mid).magnitude < 0.05f) return mid;
            float leftDis = (col.bounds.ClosestPoint(left) - left).magnitude;
            float rightDis = (col.bounds.ClosestPoint(right) - right).magnitude;
            if (leftDis < rightDis) return getCollisionPoint1(left, mid, col);
            else return getCollisionPoint1(right, mid, col);
        }*/
    public bool removePrePoint(Vector3 point)
    {
        if(Vector3.Distance(point, pivotPoints[pivotPoints.Count - 2]) > lineRenderer.endWidth)
        {
            RaycastHit2D hit2 = Physics2D.Raycast(pivotPoints[pivotPoints.Count - 3], point - pivotPoints[pivotPoints.Count - 3], Vector3.Distance(point, pivotPoints[pivotPoints.Count - 3]));
            if (!hit2)
            {
                return removePrePoint((point + pivotPoints[pivotPoints.Count - 2]) / 2);
            } else
            {
                if (hit2.collider.gameObject.tag == "Barrier")
                {
                    return false;
                } else
                {
                    //Debug.Log((Vector3)((point + pivotPoints[pivotPoints.Count - 2]) / 2));
                    return removePrePoint((point + pivotPoints[pivotPoints.Count - 2]) / 2);
                }
            } 
        } else {
            return true;
        }
    }
}
