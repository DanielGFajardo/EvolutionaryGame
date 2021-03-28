
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
    private GameObject[] cubes;
    public int dimention; 
    public GameObject EGOMAZE;
    public GameObject shield;
    
    // Start is called before the first frame update
    void Start()
    {
        digitalizeRandomMap();
        //StartCoroutine(IncreaseLight(40,5));
        //StartCoroutine(MapChange());
    }

    void digitalizeRandomMap(){
        DirectoryInfo dir = new DirectoryInfo("Assets/EG-main-2/Scripts/Maps/maps");
        FileInfo[] info = dir.GetFiles("*.*");
        bool EGOAllcoated = false;
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
        List<GameObject> listOfCubes = new List<GameObject>();
        for (int i = 0; i <dimention; i++){
            for (int a = 0; a <dimention; a++){
                if(mapValues[i*50+a] == "0.0"){
                    GameObject blockL1 = Instantiate(block1, new Vector3(i,0,a), block1.transform.rotation) as GameObject;
                    GameObject blockL2 = Instantiate(block1, new Vector3(i,1,a), block1.transform.rotation) as GameObject;
                    listOfLights.Add(blockL1.transform.GetChild(6).GetComponentInChildren<Light>());
                    listOfLights.Add(blockL2.transform.GetChild(6).GetComponentInChildren<Light>());
                    listOfCubes.Add(blockL1);
                    listOfCubes.Add(blockL2);
                }
                else if(mapValues[i*50+a] == "3.0"){
                    GameObject fountain1 = Instantiate(fountain, new Vector3(i,-0.81f,a),fountain.transform.rotation) as GameObject;
                }
                else if(mapValues[i*50+a] == "4.0" && !(EGOAllcoated)){
                    shield.transform.position=new Vector3(i,-0.81f,a);
                    EGOMAZE.transform.position=new Vector3(i,-0.81f,a);
                    EGOAllcoated = true;
                }
            }
        }
        lights = listOfLights.ToArray();
        cubes = listOfCubes.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }

    public void ChangeMap(){
        StartCoroutine(IncreaseLight(40,5));
    }

    IEnumerator MapChange(){
        Debug.Log("Map Change");
        Coroutine co;
        for(int i = 1;i<4;i++){
            Debug.Log(i);
            co = StartCoroutine(BeepFrequency(1*i));
            yield return new WaitForSeconds(10);
            StopCoroutine(co);
        }
        digitalizeRandomMap();
    }

    IEnumerator BeepFrequency(int frequency){
        bool Activated = true;
        while(true){
            if(Activated){
                foreach (Light light in lights){
                    light.intensity=0;
                    Activated = false;
                }
                yield return new WaitForSeconds(0.2f);
            }
            else{
                foreach (Light light in lights){
                    light.intensity=5;
                    Activated = true;
                }
                yield return new WaitForSeconds((float)(1-(frequency*0.2f)));
            }
        }
    }
    IEnumerator IncreaseLight(int LimitUp,int LimitLow){
        Debug.Log("Increase");
        for(int i = 1;i<LimitUp;i++){
            foreach (Light light in lights){
                light.range=light.range+1;
                light.intensity=light.intensity+1;
            }
            yield return new WaitForSeconds(0.075f);
        }
        Light[] lightsOld=lights;
        GameObject[] cubesOld=cubes;
        digitalizeRandomMap();
        Debug.Log("Decrease");
        for(int i = 40;i>LimitLow-1;i--){
            foreach (Light light in lightsOld){
                light.range=light.range-1;
                light.intensity=light.intensity-1;
            }
            yield return new WaitForSeconds(0.075f);
        }
        foreach (GameObject cube in cubesOld){
            Destroy(cube);
        }
    }
}
