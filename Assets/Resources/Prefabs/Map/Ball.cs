using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    public GameObject go;
    int nowon = 0;//�{�b�b���Ӥ��
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
        if (test < nowon + 1 && test < createMAP.self.length)
        {
            goahead();
            test++;
            Invoke("nowup", speed);
        }
    }

    void goahead()
    {
        GameObject nextCube = go.transform.GetChild(nowon + 1).gameObject;
        ball.transform.DOMove(nextCube.transform.position, speed).SetEase(Ease.Linear);
        Debug.Log(test);

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