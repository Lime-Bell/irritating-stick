using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public string sceneName;

    public RawImage maskImage;

    public Button startButton;
    public GameObject gameOverText;
    public GameObject clearText;
    public GameObject retryButton;
    public GameObject nextButton;
    public GameObject exitButton;

    // Start is called before the first frame update
    void Start()
    {
        maskImage.gameObject.SetActive(true);
        //maskImage.color = Color.black;

        //gameOverText.SetActive(false);
        //clearText.SetActive(false);

        startButton.gameObject.SetActive(true);
        //retryButton.SetActive(false);
        //nextButton.SetActive(false);
        //exitButton.SetActive(false);

        startButton.onClick.AddListener(StartButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        startButton.gameObject.SetActive(false);
        StartCoroutine(StartCountdown());
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private System.Collections.IEnumerator StartCountdown()
    {

        float duration = 3f; 
        float elapsedTime = 0f;
        //Color startColor = Color.black;
        Color endColor = Color.clear;

        while (elapsedTime < duration)
        {
            maskImage.color = Color.Lerp(maskImage.color, endColor, elapsedTime / duration * 0.02f);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        maskImage.color = endColor;

        Debug.Log("Finish");

        //yield return new WaitForSeconds(3f);

        maskImage.gameObject.SetActive(false);


        

    }
}
