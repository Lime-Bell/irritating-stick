using System.Collections;
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
      /*new int[]{ 0,0,0,1,1,1,2,2,0,0,3,3,3,0,0,4,4,0,0 },
      new int[]{ 0,0,1,1,0,0,2,2,3,3,4,4,0,0 },
      new int[]{ 0,0,0,3,3,2,2,0,0,1,1,1,4,4,0,0 },
      new int[]{ 0,0,3,3,3,4,4,0,0,1,1,1,0,0,2,2,0,0 }*/
      new int[]{0,0,0,3,3,3,2,2,2,2,2,2,2,2,0,0,0,4,4,4,4,4,4,4,4,0,0,1,1,1,0,0,0},
      new int[]{0,0,0,0,1,1,1,2,2,2,3,3,3,3,3,3,0,0,0,4,4,4,1,1,1,0,0,0},
      new int[]{0,0,0,1,1,1,1,1,2,2,2,2,0,0,0,4,4,4,4,3,3,3,3,3,0,0,0},
      new int[]{0,0,0,0,1,1,1,1,2,2,2,2,0,0,0,3,3,3,3,4,4,4,4,0,0,0},
      new int[]{0,0,0,0,3,3,3,3,4,4,4,4,0,0,0,1,1,1,1,2,2,2,2,0,0,0}
    };
    void MapDesign()
    {
        unit_mix = new List<int>();
        for(int i=0; i < unit_num; i++)
        {
            int r = Random.Range(0,mapunit.Count);
            for (int j = 0; j < mapunit[r].Length; j++)
                unit_mix.Add(mapunit[r][j]);
        }
        length = unit_mix.Count;

    }
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
        
        //length = Cube.Length;
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
           
        }
        
    }

    public void Create()
    {
        //float x = 0, y = 0, z = 0;
        Vector3 xyz = new Vector3(0, 0, 0);
        //int p = length / turn_num;//代表多久轉一次彎，5可以換其他數字
        //float a = 0, b = 0, c = 0;
        //int Case = 0;
        List<Vector3> Vectorplus = new List<Vector3> 
        {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0), 
            new Vector3(0, 0, 1),
            new Vector3(0, -1, 0), 
            new Vector3(0, 0, -1) 
        };
        bool endPoint = false;
        
        Cube = new List<GameObject>();
        for (int i = 0; i < length; i++)
        {
            if (i == 0)
            {
                //Debug.Log(x+" "+ y +" " + z);
                //Cube[i] = Instantiate(Resources.Load("Prefabs/Map/StartCube"), go.transform) as GameObject;
                Cube.Add(Instantiate(Resources.Load("Prefabs/Map/StartCube"), go.transform) as GameObject);
            }
            else if (i == length - 1)
            {
                //Cube[i] = Instantiate(Resources.Load("Prefabs/Map/GoalCube"), go.transform) as GameObject;
                Cube.Add(Instantiate(Resources.Load("Prefabs/Map/GoalCube"), go.transform) as GameObject);
                endPoint = true;
            }
            else if (i > 0 && i < length - 1)
            {
                Cube.Add(Instantiate(Resources.Load("Prefabs/Map/TrackCube"), go.transform) as GameObject);
            }


            //Cube[i] = Instantiate(Resources.Load("Prefabs/Map/TrackCube"), go.transform) as GameObject;//"Prefabs/Cube"要改成你軌道方塊的Prefab的路徑

        /*    int precase = Case;
            //if (i % p == 0) Case = Random.Range(0, 5);
            Case = unit_mix[i];
            switch (Case)
            {
                case 0:
                    a = 1;
                    b = 0;
                    c = 0;
                    if (i == 0) a = 0;
                    break;
                case 1:
                    a = 0;
                    b = 1;
                    c = 0;
                    if (precase == 3) { b = 0; a = 1; Case = 0; }
                    break;
                case 2:
                    a = 0;
                    b = 0;
                    c = 1;
                    if (precase == 4) { c = 0; a = 1; Case = 0; }
                    break;
                case 3:
                    a = 0;
                    b = -1;
                    c = 0;
                    if (precase == 1) { b = 0; a = 1; Case = 0; }
                    break;
                case 4:
                    a = 0;
                    b = 0;
                    c = -1;
                    if (precase == 2) { c = 0; a = 1; Case = 0; }
                    break;

            
                    //Cube[i].transform.position = new Vector3(x + a, y + b, z + c);
            }*/
            Cube[i].transform.localPosition = xyz;
            if (i > 0)
                Cube[i].transform.localPosition += Vectorplus[unit_mix[i]];
            xyz = Cube[i].GetComponent<Transform>().localPosition;

            /* if (i % p == p - 1)
             {
                 for (int j = p - 1; j < i; j += p)
                 {
                     if (Cube[i].transform.position == Cube[j].transform.position)
                     {
                         float i_1_x = Cube[i - 2].GetComponent<Transform>().localPosition.x + 1;
                         float i_1_y = Cube[i - 2].GetComponent<Transform>().localPosition.y;
                         float i_1_z = Cube[i - 2].GetComponent<Transform>().localPosition.z;
                         Cube[i - 1].transform.localPosition = new Vector3(i_1_x, i_1_y, i_1_z);
                         float i_x = Cube[i - 1].GetComponent<Transform>().localPosition.x + 1;
                         float i_y = Cube[i - 1].GetComponent<Transform>().localPosition.y;
                         float i_z = Cube[i - 1].GetComponent<Transform>().localPosition.z;
                         Cube[i].transform.localPosition = new Vector3(i_x, i_y, i_z);
                     }
                 }
             }*/
            if (endPoint == true)
            {
                endPoint = false;
                Data.endPoint = Cube[i].transform.position;
            }

            /*x = Cube[i].GetComponent<Transform>().localPosition.x;
            y = Cube[i].GetComponent<Transform>().localPosition.y;
            z = Cube[i].GetComponent<Transform>().localPosition.z;
            Debug.Log(x + " " + y + " " + z);*/
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
