using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingCube : MonoBehaviour
{
    bool disappear = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (transform.localEulerAngles == new Vector3(0, 0, 90)) 
            transform.GetChild(1).transform.localPosition = new Vector3(-0.5f, 0, 0);
        else if (transform.localEulerAngles == new Vector3(0, 0, 180)) 
            transform.GetChild(1).transform.localPosition = new Vector3(0, 0.5f, 0);
        else if (transform.localEulerAngles.x == 270)  
            transform.GetChild(1).transform.localPosition = new Vector3(0, 0, -0.5f);
        else if (transform.localEulerAngles == new Vector3(0, 0, 0)) 
            transform.GetChild(1).transform.localPosition = new Vector3(0, -0.5f, 0);
        else if (transform.localEulerAngles.x == 90) 
            transform.GetChild(1).transform.localPosition = new Vector3(0, 0, 0.5f);
        transform.GetChild(1).transform.eulerAngles -= transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("BALL"))
        {
            if (disappear/*&& !GameController.self.inmap*/)
            {
                GameController.self.outside = true;
                Debug.Log("OUT");
            }
            else
            {
                GameController.self.outside = false;
                /*GameController.self.inmap = true;*/
                Debug.Log("IN");
            }
        }   
    }
    public void cubeDisappear()
    {
        disappear = true;
    }
    public void cubeAppear()
    {
        disappear = false;
    }
}
