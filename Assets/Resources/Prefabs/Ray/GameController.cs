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
            // 控制射線的移動，根據滑鼠位置或其他輸入方式來更新射線的位置
            UpdateLinePosition();

            // 檢查射線是否碰觸到軌道邊緣，根據情況執行相應的操作
            CheckTrackCollision();
        }
    }

    private void StartGame()
    {
        

        lineRenderer.SetPosition(0, new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 2, mainCamera.transform.position.z));
        lineRenderer.SetPosition(1, startObject.transform.position);
        lineRenderer.enabled = true;



        // 啟動倒數計時
        StartCoroutine(Countdown());
    }

    private System.Collections.IEnumerator Countdown()
    {

        yield return new WaitForSeconds(3f);

        gameStarted = true;
    }

    private void UpdateLinePosition()
    {
        // 根據滑鼠位置更新射線的位置
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
        // 檢查射線是否碰觸到軌道邊緣
        Vector3 linePosition = lineRenderer.GetPosition(1);
        Vector3 trackPosition = trackObject.transform.position;

        float distanceToTrack = Vector3.Distance(linePosition, trackPosition);
        //float trackRadius = trackObject.transform.localScale.x / 2f; // 假設軌道為球形，取軌道的X軸尺寸作為半徑

        //if (distanceToTrack > trackRadius)
        //{
        //    // 射線偏離軌道邊緣，執行遊戲失敗的操作
        //    //GameOver();
        //}
        //else if (Vector3.Distance(linePosition, goalObject.transform.position) < trackRadius)
        //{
        //    // 射線碰觸到終點，執行遊戲通過的操作
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
    //    // 加載下一關的場景
    //    // SceneManager.LoadScene("NextLevel");
    //}

    //private void Exit()
    //{
    //    // 關閉應用程式或返回主選單等操作
    //    // Application.Quit();
    //}
}