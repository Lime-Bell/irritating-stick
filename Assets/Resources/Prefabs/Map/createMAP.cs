using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class createMAP : MonoBehaviour
{
    //public GameObject StartCube;

    public GameObject Cube; 
    public GameObject go;   //生成在哪個GameObject下面
    public int length;
    // Start is called before the first frame update
    void Start()
    {
        Create();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create()
    {
        float x = 0, y = 0, z = 0;
        int p = length / 5;//代表多久轉一次彎，5可以換其他數字
        float a = 0, b = 0, c = 0;
        int Case = 0;

        for (int i = 0; i < length; i++)
        {
            if (i == 0)
            {
                Cube = Instantiate(Resources.Load("Prefabs/Map/StartCube"), go.transform) as GameObject;
            }
            else if (i == length - 1)
            {
                Cube = Instantiate(Resources.Load("Prefabs/Map/GoalCube"), go.transform) as GameObject;
            }
  
            else if (i > 0 && i < length - 1)
            {
                Cube = Instantiate(Resources.Load("Prefabs/Map/TrackCube"), go.transform) as GameObject;//"Prefabs/Cube"要改成你軌道方塊的Prefab的路徑

                int precase = Case;
                if (i % p == 0) Case = Random.Range(0, 5);
                switch (Case)
                {
                    case 0:
                        a = 1;
                        b = 0;
                        c = 0;
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

                }
                //Cube[i].transform.position = new Vector3(x + a, y + b, z + c);
            }
            Cube.transform.position = new Vector3(x + a, y + b, z + c);

            x = Cube.GetComponent<Transform>().position.x;
            y = Cube.GetComponent<Transform>().position.y;
            z = Cube.GetComponent<Transform>().position.z;
        }
    }

    public void DestroyMAP()
    {
        //for(int i=0;i<length;i++)
        Destroy(Cube);
    }
}
