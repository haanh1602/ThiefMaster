using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental;

public class Player : MonoBehaviour
{
    public bool GotRequiredItem = false;
    public bool Win = false;
    public bool Failed = false;
    public int level = 1;

    public PlayerData playerData = new PlayerData();

    private Vector3 startGamePosition = new Vector3();

    [SerializeField]
    public GameObject requiredItem;

    private Vector3 startPos;
    private Vector3 direction;

    [SerializeField]
    private GameObject armLine;

    [SerializeField]
    private Transform attachPos;
    [SerializeField]
    private Transform directPos;

    private Vector3 firstDirection = new Vector3();
    private Vector3 preDirect = new Vector3();

    private Rigidbody2D myRigid;

    private Color successColor = Color.green;
    private Color normalColor = Color.black;
    private Color failedColor = Color.red;

    private float deltaX, deltaY;

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
        playerData.passedLevel = SavingSystem.LoadPlayer().passedLevel;
        firstDirection = directPos.position - attachPos.position;
        preDirect = firstDirection;
    }

    private void Start()
    {
        Vector3 tmp = transform.position;
        startGamePosition = tmp;
        level = PlayerData.currentLevel;
    }

    void Update()
    {
        if(!Win && !Failed)
        {
            if (Input.touchCount > 0)
            {
                MoveController();
            }
        }
        if(Win)
        {
            float time = 6f;
            if(!Failed)
            {
                if(armLine.GetComponent<LineController>().pivotPoints.Count <= 2)
                {
                    transform.position = Vector3.MoveTowards(transform.position, startGamePosition, time * Time.deltaTime);
                    requiredItem.transform.position = Vector3.MoveTowards(requiredItem.transform.position, startGamePosition, time * Time.deltaTime);
                } else {
                    Vector3 lastPivotPoint = armLine.GetComponent<LineController>().pivotPoints[armLine.GetComponent<LineController>().pivotPoints.Count - 2];
                    if(transform.position == lastPivotPoint)
                    {
                        armLine.GetComponent<LineController>().pivotPoints.RemoveAt(armLine.GetComponent<LineController>().pivotPoints.Count - 2);
                        armLine.GetComponent<LineController>().lineRenderer.positionCount--;
                    }
                    transform.position = Vector3.MoveTowards(transform.position, lastPivotPoint, time * Time.deltaTime);
                    requiredItem.transform.position = Vector3.MoveTowards(requiredItem.transform.position, lastPivotPoint, time * Time.deltaTime);
                }
            }
            if(requiredItem.transform.position == startGamePosition)
            {
                playerData.passedLevel = Mathf.Max(playerData.passedLevel, level + 1);
                SavingSystem.SavePlayer(playerData);
                Invoke("WinScene", 0.2f);
                Invoke("ResultWinScene", 1f);
            }
        }
        if(Failed)
        {
            armLine.GetComponent<LineRenderer>().material.color = failedColor;
            myRigid.velocity = Vector2.zero;
            gameObject.isStatic = true;
            Invoke("FailedScene", 0.2f);
            Invoke("ResultFailedScene", 1f);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RequiredItem"))
        {
            armLine.GetComponent<LineRenderer>().material.color = successColor;
            GotRequiredItem = true;
        }
        if (collision.gameObject.CompareTag("Objects"))
        {
            Failed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RequiredItem"))
        {
            armLine.GetComponent<LineRenderer>().material.color = normalColor;
            GotRequiredItem = false;
        }
    }

    void MoveController()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                deltaX = touchPos.x - transform.position.x;
                deltaY = touchPos.y - transform.position.y;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                //armLine.GetComponent<LineController>().AddPivotPoints(new Vector3(touchPos.x - deltaX, touchPos.y - deltaY, 0), attachPos.position);
                myRigid.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
            }
            if (touch.phase == TouchPhase.Ended)
            {
                myRigid.velocity = Vector2.zero;
            }
            if (touch.phase == TouchPhase.Ended && GotRequiredItem)
            {
                Win = true;
                requiredItem.transform.position = Vector3.MoveTowards(requiredItem.transform.position, attachPos.position, 0.5f);
                armLine.GetComponent<LineRenderer>().material.color = normalColor;
                GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }

    private void FirstMoveController()
    {
        Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero
        if (touch.phase == TouchPhase.Began)
        {
            startPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
        }
        if (touch.phase == TouchPhase.Moved)
        {
            // get the touch position from the screen touch to world point
            Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            direction = touchedPos - startPos;
            transform.Rotate(0, 0, Vector3.SignedAngle(preDirect, direction, new Vector3(0, 0, 1)));
            preDirect = direction;
            startPos = touchedPos;
            myRigid.velocity = new Vector2(direction.x / Time.deltaTime, direction.y / Time.deltaTime);
        }
        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended)
        {
            direction = new Vector3(0, 0, 0);
            myRigid.velocity = Vector2.zero;
        }
        if (touch.phase == TouchPhase.Ended && GotRequiredItem)
        {
            Win = true;
            requiredItem.transform.position = Vector3.MoveTowards(requiredItem.transform.position, attachPos.position, 100f);
            armLine.GetComponent<LineRenderer>().material.color = normalColor;
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void WinScene()
    {
        GetComponent<ResultSceneController>().Win();
    }

    private void ResultWinScene()
    {
        new SceneChanger().ChangeScene("WinScene") ;
    }

    void FailedScene()
    {
        GetComponent<ResultSceneController>().Failed();
    }

    private void ResultFailedScene()
    {
        new SceneChanger().ChangeScene("FailedScene");
    }

    private float LimitedVelocity(float v)
    {
        // Limit at 0.1
        if(v < 0f)
        {
            return Mathf.Max(v, -0.2f);
        } else
        {
            return Mathf.Min(v, 0.2f);
        }
    }

    private void OnEnable()
    {
        this.enabled = true;
    }

    private void OnDisable()
    {
        this.enabled = false;
    }

}
