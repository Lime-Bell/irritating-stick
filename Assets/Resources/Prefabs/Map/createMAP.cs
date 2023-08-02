﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class createMAP : MonoBehaviour
{
    //public GameObject StartCube;
    public static createMAP self;
    public List<GameObject> Cube = new List<GameObject>(); 
    public GameObject go;   //生成在哪個GameObject下面
    public int length;
    public int unit_num = 3;
    List<int> unit_mix = new List<int>();
    public int turn_num = 5;
    public GameObject ball;

    List<int[]> mapunit = new List<int[]>
    { 
      
      new int[]{0,0,0,3,3,3,2,2,2,2,2,2,2,2,0,0,0,4,4,4,4,4,4,4,4,0,0,1,1,1,0,0,0},
      new int[]{0,0,0,0,1,1,1,2,2,2,3,3,3,3,3,3,0,0,0,4,4,4,1,1,1,0,0,0},
      new int[]{0,0,0,1,1,1,1,1,2,2,2,2,0,0,0,4,4,4,4,3,3,3,3,3,0,0,0},
      new int[]{0,0,0,0,1,1,1,1,2,2,2,2,0,0,0,3,3,3,3,4,4,4,4,0,0,0},
      new int[]{0,0,0,0,3,3,3,3,4,4,4,4,0,0,0,1,1,1,1,2,2,2,2,0,0,0}
    };
    
    private void Awake()
    {
        if (self == null)
        {
            self = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.detroyMap)
        {
            Data.detroyMap = false;

            DestroyMAP();
            ball.SetActive(false);

        }
        if (Data.createMap)
        {
            Data.createMap = false;
            MapDesign();
            Create();
            ball.SetActive(true);
            ball.transform.eulerAngles = Cube[0].gameObject.transform.eulerAngles;

        }
        
    }

    void MapDesign()
    {
        unit_mix = new List<int>();
        for (int i = 0; i < unit_num; i++)
        {
            int r = Random.Range(0, mapunit.Count);
            for (int j = 0; j < mapunit[r].Length; j++)
                unit_mix.Add(mapunit[r][j]);
        }
        length = unit_mix.Count;

    }
    public void Create()
    {
        Vector3 xyz = new Vector3(0, 0, 0);
        
        List<Vector3> Vectorplus = new List<Vector3> 
        {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0), 
            new Vector3(0, 0, 1),
            new Vector3(0, -1, 0), 
            new Vector3(0, 0, -1) 
        };
        List<Vector3> Angle = new List<Vector3>
        {
            new Vector3(0, 0, 90),
            new Vector3(0, 0, 180),
            new Vector3(-90, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(90, 0, 0)
        };
        bool endPoint = false;
        
        Cube = new List<GameObject>();
        for (int i = 0; i < length; i++)
        {
            if (i == 0)
            {
                
                Cube.Add(Instantiate(Resources.Load("Prefabs/Map/StartCube"), go.transform) as GameObject);
            }
            else if (i == length - 1)
            {
                
                Cube.Add(Instantiate(Resources.Load("Prefabs/Map/GoalCube"), go.transform) as GameObject);
                endPoint = true;
            }
            else if (i > 0 && i < length - 1 && unit_mix[i + 1] == unit_mix[i])
            {
                Cube.Add(Instantiate(Resources.Load("Prefabs/Map/TrackCube"), go.transform) as GameObject);
            }
            else if (i < length - 1 && unit_mix[i + 1] != unit_mix[i]) 
            {
                Cube.Add(Instantiate(Resources.Load("Prefabs/Map/TURN"), go.transform) as GameObject);
            }

           
            Cube[i].transform.localPosition = xyz;
            if (i > 0) {
                Cube[i].transform.localPosition += Vectorplus[unit_mix[i]];
                Cube[i].transform.eulerAngles = Angle[unit_mix[i]];
            }
            xyz = Cube[i].GetComponent<Transform>().localPosition;

            
            if (endPoint == true)
            {
                endPoint = false;
                Data.endPoint = Cube[i].transform.position;
            }

        }
    }

    public void DestroyMAP()
    {
        for (int i = 0; i < length; i++)
        {
            Destroy(transform.GetChild(i+1).gameObject);
        }
    }
}
