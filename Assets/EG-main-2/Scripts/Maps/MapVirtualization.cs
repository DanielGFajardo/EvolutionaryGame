﻿
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
public class MapVirtualization : MonoBehaviour
{
    private string[] mapValues;
    public GameObject block1;
    public GameObject fountain;
    private Light[] lights;
    public int dimention; 
    public bool Activated;
    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/EG-main-2/Scripts/Maps/maps");
        FileInfo[] info = dir.GetFiles("*.*");
        Debug.Log(info.Length/2);
        int targetMap = Random.Range(1,info.Length/2);
        string data = System.IO.File.ReadAllText ("Assets/EG-main-2/Scripts/Maps/maps/map"+targetMap.ToString()+".csv");
        mapValues = data.Split(new char [] {',' , '\n' }, StringSplitOptions.RemoveEmptyEntries);
        //for (int i = 0; i < words.Length; i++)
        //   Debug.Log(words[i]);
        //for (int i = 0; i <dimention; i++){
        //    for (int a = 0; a <dimention; a++){
        //        GameObject block = Instantiate(block1, new Vector3(i,0,a), block1.transform.rotation) as GameObject;
        //    }
        //}
        List<Light> listOfLights = new List<Light>();
        for (int i = 0; i <dimention; i++){
            for (int a = 0; a <dimention; a++){
                Debug.Log(mapValues[i*50+a]);
                if(mapValues[i*50+a] == "0.0"){
                    GameObject blockL1 = Instantiate(block1, new Vector3(i,0,a), block1.transform.rotation) as GameObject;
                    GameObject blockL2 = Instantiate(block1, new Vector3(i,1,a), block1.transform.rotation) as GameObject;
                    listOfLights.Add(blockL1.transform.GetChild(6).GetComponentInChildren<Light>());
                    listOfLights.Add(blockL2.transform.GetChild(6).GetComponentInChildren<Light>());
                }
                else if(mapValues[i*50+a] == "3.0"){
                    GameObject fountain1 = Instantiate(fountain, new Vector3(i,-0.81f,a),fountain.transform.rotation) as GameObject;
                }
            }
        }
        lights = listOfLights.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Activated){
            foreach (Light light in lights){
                light.intensity=0;
                Activated = false;
            }
        }
        else{
            foreach (Light light in lights){
                light.intensity=5;
                Activated = true;
            }
        }
        */
    }
}
