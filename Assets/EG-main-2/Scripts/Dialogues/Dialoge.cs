using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//based on the tutorial https://www.youtube.com/watch?v=f-oSXg6_AMQ
public class Dialoge : MonoBehaviour
{

    public TextMeshProUGUI textd;
    public string[] riddles;
    private int index;
    public float type;
    public GameObject[] initialise;

    void Start()
    {
        StartCoroutine(Type());

    }

    IEnumerator Type()
    {
        foreach(char letter in riddles[index].ToCharArray())
        {
           textd.text += letter;
           yield return new WaitForSeconds(type);
        }
    }
    public void NextSentence()
    {
        if (index < riddles.Length - 1)
        {
            index=1;
            textd.text = "";
            StartCoroutine(Type());
            foreach (GameObject k in initialise) {
                k.SetActive(true); }
        }
        else
        {
            textd.text = "";

        }
    }
    public void NextSentence2()
    {
        if (index < riddles.Length - 1)
        {
            index=2;
            textd.text = "";
            StartCoroutine(Type());
            index = 0;
        }
        else
        {
            textd.text = "";

        }
    }
}
