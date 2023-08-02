using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    public GameObject go;
    int nowon = 0;//現在在哪個方塊
    bool stop = false;
    int test = 0;
    float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.self.playing)
        {
            ball.transform.position = go.transform.GetComponent<Transform>().position;
            nowon = 0;
            test = 0;
            ball.transform.eulerAngles = go.transform.GetChild(1).gameObject.transform.eulerAngles;
            
        }
        if (GameController.self.playing)
        {
            if (test < nowon + 1 && test < createMAP.self.length && GameController.self.moveball)
            {
                goahead();
                test++;
                Invoke("nowup", speed);
            }
            else if(!GameController.self.moveball)
            {
                Debug.Log("STOP BALL MOVE");
            }
        }
        
    }

    void goahead()
    {
        GameObject nextCube = go.transform.GetChild(nowon + 1).gameObject;
        ball.transform.DOMove(nextCube.transform.position, speed).SetEase(Ease.Linear);
        ball.transform.DORotate(nextCube.transform.eulerAngles, speed);
        
    }
    void nowup()
    {
        nowon++;
    }

    void stoping()
    {
        stop = true;
    }
    void going()
    {
        stop = false;
    }
}
