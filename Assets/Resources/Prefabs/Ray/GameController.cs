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

    //public GameObject gameOverPanel;
    //public GameObject clearPanel;
    //public Button retryButton;
    //public Button nextButton;
    //public Button exitButton;

    //public GameObject mask;
    public GameObject gameOverText;
    public GameObject clearText;

    public GameObject startButton;
    public GameObject retryButton;
    public GameObject nextButton;
    public GameObject exitButton;
    public GameObject playerLine;
    public GameObject goal;

    public RawImage mask;

    private LineRenderer lineRenderer;

    private bool gameStarted = false;
    private bool gameOver = false;
    private bool gameClear = false;

    private Camera mainCamera;
    private Vector3 lastMousePosition;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        mainCamera = Camera.main;

        gameStarted = false;

        //retryButton.onClick.AddListener(Retry);
        //nextButton.onClick.AddListener(Next);
        //exitButton.onClick.AddListener(Exit);

        //mask.gameObject.SetActive(true);
        //mask.color = Color.black;
        //startButton.SetActive(true);

        //gameOverText.SetActive(false);
        //clearText.SetActive(false);
        //retryButton.SetActive(false);
        //nextButton.SetActive(false);
        //exitButton.SetActive(false);

        //playerLine.SetActive(false);
        //goal.SetActive(false);

        StartGame();
    }

    private void Update()
    {
        if (gameStarted && !gameOver && !gameClear)
        {
            // ����g�u�����ʡA�ھڷƹ���m�Ψ�L��J�覡�ӧ�s�g�u����m
            UpdateLinePosition();

            // �ˬd�g�u�O�_�IĲ��y�D��t�A�ھڱ��p����������ާ@
            CheckTrackCollision();
        }
    }

    private void StartGame()
    {
        

        lineRenderer.SetPosition(0, new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 2, mainCamera.transform.position.z));
        lineRenderer.SetPosition(1, startObject.transform.position);
        lineRenderer.enabled = true;



        // �Ұʭ˼ƭp��
        StartCoroutine(Countdown());
    }

    private System.Collections.IEnumerator Countdown()
    {

        yield return new WaitForSeconds(3f);

        gameStarted = true;
    }

    private void UpdateLinePosition()
    {
        // �ھڷƹ���m��s�g�u����m
        Vector3 mousePosition = Input.mousePosition;
        Vector3 rayOriginPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 2, mainCamera.transform.position.z);

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            lineRenderer.SetPosition(0, rayOriginPosition);
            lineRenderer.SetPosition(1, hit.point);
        }
    }

    private void CheckTrackCollision()
    {
        // �ˬd�g�u�O�_�IĲ��y�D��t
        Vector3 linePosition = lineRenderer.GetPosition(1);
        Vector3 trackPosition = trackObject.transform.position;

        float distanceToTrack = Vector3.Distance(linePosition, trackPosition);
        //float trackRadius = trackObject.transform.localScale.x / 2f; // ���]�y�D���y�ΡA���y�D��X�b�ؤo�@���b�|

        //if (distanceToTrack > trackRadius)
        //{
        //    // �g�u�����y�D��t�A����C�����Ѫ��ާ@
        //    //GameOver();
        //}
        //else if (Vector3.Distance(linePosition, goalObject.transform.position) < trackRadius)
        //{
        //    // �g�u�IĲ����I�A����C���q�L���ާ@
        //    //GameClear();
        //}
    }


    //private void GameOver()
    //{
    //    gameOverPanel.SetActive(true);
    //    gameOver = true;
    //}

    //private void GameClear()
    //{
    //    clearPanel.SetActive(true);
    //    gameClear = true;
    //}

    //private void Retry()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

    //private void Next()
    //{
    //    // �[���U�@��������
    //    // SceneManager.LoadScene("NextLevel");
    //}

    //private void Exit()
    //{
    //    // �������ε{���Ϊ�^�D��浥�ާ@
    //    // Application.Quit();
    //}
}