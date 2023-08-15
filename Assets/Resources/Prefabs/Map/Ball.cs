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
    float speed = 1f;
    int target=0;
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
            speed = 1f;
        }
        if (GameController.self.playing)
        {
            if (test < nowon+1  && test < createMAP.self.length && GameController.self.moveball)
            {
                target = nowon + 1;
                goahead();
                test++;
                Invoke("nowup", speed);
            }
            else if(!GameController.self.moveball)
            {
               // target = nowon;
               // DOTween.Pause(transform);
                Debug.Log("STOP BALL MOVE");
            }
        }
        
    }

    void goahead()
    {
        GameObject nextCube = go.transform.GetChild(target).gameObject;
        ball.transform.DOMove(nextCube.transform.position, speed).SetEase(Ease.Linear);
        ball.transform.DORotate(nextCube.transform.eulerAngles, 0.3f);
        
    }
    void nowup()
    {
        nowon++;
        target++;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("SPEEDDOWN"))
        {
            if(speed<1.8f)
                speed *= 1.2f;
            col.gameObject.SetActive(false);
            Audio.self.PlaySound("speeddown");
        }
        if (col.CompareTag("SPEEDUP"))
        {
            speed *= 0.8f;
            col.gameObject.SetActive(false);
            Audio.self.PlaySound("speedup");
        }
        if (col.CompareTag("MAP"))
        {
            GameController.self.outside = false;
      
        }
       /* if (col.CompareTag("SPECIAL"))
        {
            GameController.self.inmap = false;
        }*/

            Debug.Log("alive");
    }
   /* private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("MAP"))
        {
            GameController.self.inmap = true;
        }
    }*/
}
