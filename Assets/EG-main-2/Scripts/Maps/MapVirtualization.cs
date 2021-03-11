using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class MapVirtualization : MonoBehaviour
{
    private string[] mapValues;
    public GameObject block1;
    public int dimention; 
    // Start is called before the first frame update
    void Start()
    {
        string data = System.IO.File.ReadAllText ("Assets/EG-main-2/Scripts/Map/map.csv");
        mapValues = data.Split(new char [] {',' , '\n' }, StringSplitOptions.RemoveEmptyEntries);
        //for (int i = 0; i < words.Length; i++)
        //   Debug.Log(words[i]);
        //for (int i = 0; i <dimention; i++){
        //    for (int a = 0; a <dimention; a++){
        //        GameObject block = Instantiate(block1, new Vector3(i,0,a), block1.transform.rotation) as GameObject;
        //    }
        //}
        for (int i = 0; i <dimention; i++){
            for (int a = 0; a <dimention; a++){
                Debug.Log(mapValues[i*50+a]);
                if(mapValues[i*50+a] == "0.0"){
                    GameObject blockL1 = Instantiate(block1, new Vector3(i,0,a), block1.transform.rotation) as GameObject;
                    GameObject blockL2 = Instantiate(block1, new Vector3(i,1,a), block1.transform.rotation) as GameObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
