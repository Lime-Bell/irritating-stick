using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject startObject;
    public GameObject trackObject;
    public GameObject goalObject;

    public Text gameOverText;
    public RawImage mask;

    public LineRenderer lineRenderer;

    public float toleranceTime;
    private float edgeTime = 0;
    private float endTime = 0;
    private bool touchEdge = false;
    //private MeshCollider meshCollider;

    private Camera mainCamera;

    private float distanceThreshold = 0.2f;

    private bool lose = false;
    private bool inmap = true;
    public bool start = false;
    public bool playing = false;
    public static GameController self;
    public bool startfall = false;
    public bool moveball=false;
    LayerMask masking = (1 << 3) | (1 << 6) ;


    private void Awake()
    {
        if (self == null)
        {
            self = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = true;
        lineRenderer.gameObject.SetActive(true);

        mainCamera = Camera.main;

       
    }

    private void Update()
    {
        
        if(start == true)
        {
            StartGame();
            start = false;
        }
        if (Data.moveRay)
        {
            // 控制射線的移動，根據滑鼠位置來更新射線的位置
            UpdateLinePosition();

            lineRenderer.enabled = true;
        }
        else
        {

            lineRenderer.enabled = false;
        }
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        Transform stoppoint;
        stoppoint = createMAP.self.transform;
        if (playing)
        {
            masking = (0 << 6) | (1 << 3);
            Debug.Log("step1");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, masking))
            {
                Debug.Log("step2");
                if (hit.collider.CompareTag("BALL"))
                {
                    Debug.Log("HIT");
                    moveball = true;
                    Debug.Log("step3");
                }


            }
            else
            {
                moveball = false;
                masking = (1 << 6) | (1 << 3);
            }
            
        }
        /*void rego()
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, masking) && hit.collider.transform == stoppoint && playing)
            {
                moveball = true;
                masking = (0 << 6) | (1 << 3) | (1 << 7);
                Debug.Log("step6");
            }
        }*/
       
    }

    private void StartGame()
    {
        lineRenderer.SetPosition(0, new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 2, mainCamera.transform.position.z));
        lineRenderer.SetPosition(1, startObject.transform.position);
        lineRenderer.enabled = true;
        masking = (1 << 3) | (0 << 6);
    }


    
    // 根據滑鼠位置更新射線的位置
    private void UpdateLinePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 rayOriginPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 2, mainCamera.transform.position.z);

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (start == false && playing == false && Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("START"))
            {
                //playing = true;
                start = true;
                moveball = true;
            }
        }

        if (Physics.Raycast(ray, out hit,Mathf.Infinity,masking))
        {
            lineRenderer.SetPosition(0, rayOriginPosition);
            lineRenderer.SetPosition(1, hit.point);
            touchEdge = false;
            
            //Debug.Log(hit.point);

            if (Data.endPoint.x - 0.7f < hit.point.x && Data.endPoint.x + 0.7f > hit.point.x
                && Data.endPoint.y - 0.7f < hit.point.y && Data.endPoint.y + 0.7f > hit.point.y
                && Data.endPoint.z - 0.7f < hit.point.z && Data.endPoint.z + 0.7f > hit.point.z)
            {
                endTime += Time.deltaTime;
            }
            //StopCoroutine(GameOverCountdown());
        }
        else
        {
            touchEdge = true;

            //StartCoroutine(GameOverCountdown());
            Vector3 targetPosition = ray.origin + ray.direction * 100f;
            lineRenderer.SetPosition(0, rayOriginPosition);
            lineRenderer.SetPosition(1, targetPosition);
        }

        if (playing == true && moveball)
        {
            if (endTime >= 1f)
            {
                endTime = 0f;
                Debug.Log("GameClear");
                GameClear();
            }
        }

        if(playing == true && !moveball)
        {
            if (touchEdge)
            {
                edgeTime += Time.deltaTime;
            }
           /*f (endTime >= 1f)
            {
                endTime = 0f;
                Debug.Log("GameClear");
                GameClear();
            }
           */
            if (edgeTime >= toleranceTime)
            {
                edgeTime = 0;
                Debug.Log("GameOver");
                GameOver();
                playing = false;
            }
            else if (!touchEdge)
            {
                edgeTime = 0;
            }
        }
        else
        {
            if (touchEdge)
            {
                edgeTime += Time.deltaTime;
            }
            if (edgeTime >= toleranceTime)
            {
                edgeTime = 0;
               
                startfall = true;
            }
        }
        

        //Debug.Log("超出邊界 " + edgeTime + " 秒!");

    }

    private void GameOver()
    {
            Data.showEndGame = true;
            Data.moveRay = false;
            Data.moveCamera = false;
            Data.showEndText = "GameOver";
            playing = false;
    }

    private void GameClear()
    {
        Data.showEndGame = true;
        Data.moveRay = false;
        Data.moveCamera = false;
        Data.showEndText = "GameClear";
        playing = false;
    }
}