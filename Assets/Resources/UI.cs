using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{
    // 要加載的場景名稱
    public string sceneName;

    public RawImage maskImage;
    public Text countdownText;

    public Button startButton;
    public Button retryButton;
    public Button nextButton;
    public Button exitButton;

    public Text endGameText;
    public Text gameClearText;



    private void Start()
    { 

        countdownText.gameObject.SetActive(false);
        endGameText.gameObject.SetActive(false);

        countdownText.color = new Color(1f, 0.84f, 0f, 0f); // 金色
        endGameText.color = new Color(1f, 0.84f, 0f, 0f);


        //maskImage.color = Color.black;


        //clearText.SetActive(false);

        startButton.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        startButton.onClick.AddListener(StartButton);
        retryButton.onClick.AddListener(RetryButton);
        nextButton.onClick.AddListener(NextButton);
        exitButton.onClick.AddListener(ExitButton);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Data.showEndGame)
        {
            endGameText.text = Data.showEndText;

            retryButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);

            endGameText.gameObject.SetActive(true);
            StartCoroutine(GameOverLerp());
        }


        
    }

    public void RetryButton()
    {
        //Data.showNextButton = false;
        Data.showEndGame = false;
        Data.resetCamera = true;

        closeUI();

        countdownText.gameObject.SetActive(true);
        endGameText.gameObject.SetActive(false);


        StartCoroutine(ReadyCountdown());
    }

    public void NextButton()
    {
        //Data.showNextButton = false;
        Data.showEndGame = false;

        Data.detroyMap = true;
        Data.createMap = true;
        Data.resetCamera = true;

        closeUI();

        countdownText.gameObject.SetActive(true);
        endGameText.gameObject.SetActive(false);

        StartCoroutine(ReadyCountdown());

    }
    public void ExitButton()
    {
        Data.showEndGame = false;
        Data.resetCamera = true;
        Data.detroyMap = true;

        closeUI();

        startButton.gameObject.SetActive(true);
        endGameText.gameObject.SetActive(false);
    }


    public void StartButton()
    {
        Data.createMap = true;

        startButton.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(true);
        
        StartCoroutine(ReadyCountdown());
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void closeUI()
    {
        startButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    private IEnumerator GameOverLerp()
    {
        
        float duration = 2f;
        float elapsedTime = 0f;
        float startAlpha = 0f;
        float endAlpha = 1f;
        float currentAlpha;


        while (elapsedTime < duration)
        {

            elapsedTime += Time.deltaTime;

            float t = elapsedTime / 2;

            currentAlpha = Mathf.Lerp(startAlpha, endAlpha, Mathf.SmoothStep(0f, 1f, t));

            Color textColor = new Color(endGameText.color.r, endGameText.color.g, endGameText.color.b, currentAlpha);
            endGameText.color = textColor;

            yield return null;
        }
    }

    private IEnumerator ReadyCountdown()
    {
        float duration = 3f;
        float elapsedTime = 0f;
        float startAlpha = 0f;
        float midAlpha = 1f;
        float endAlpha = 0f;
        float currentAlpha;

        float midPoint = duration / 2f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            int countdownNumber = Mathf.CeilToInt(duration - elapsedTime);
            countdownText.text = countdownNumber.ToString();

            float t = elapsedTime - Mathf.FloorToInt(elapsedTime) / 1;

            

            int group = ((int)(elapsedTime / 0.25)) % 4;

            if (group == 0 || group == 1)
            {
                currentAlpha = Mathf.Lerp(startAlpha, midAlpha, Mathf.SmoothStep(0f, 1f, t));
            }
            else
            {
                currentAlpha = Mathf.Lerp(midAlpha, endAlpha, Mathf.SmoothStep(0f, 1f, t));
            }

            Color textColor = new Color(countdownText.color.r, countdownText.color.g, countdownText.color.b, currentAlpha);

            countdownText.color = textColor;

            yield return null;
        }

        Debug.Log("Finish");

        countdownText.gameObject.SetActive(false);
        maskImage.gameObject.SetActive(false);
        Data.moveRay = true;
        Data.moveCamera = true;

    }
}
