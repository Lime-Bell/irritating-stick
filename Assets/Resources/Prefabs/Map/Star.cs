using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles -= transform.parent.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
