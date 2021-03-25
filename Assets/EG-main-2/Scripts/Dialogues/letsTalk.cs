using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letsTalk : MonoBehaviour
{
    public GameObject dialoge;
    public GameObject[] canvas;
    //When the character reaches the "floor" after jumping can jump is set to true and the jump action is replaced by running or idle
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject k in canvas)
            {
                k.SetActive(true);
            }
            
            dialoge.SetActive(true);

        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject k in canvas)
            {
                k.SetActive(false);
            }
        }


    }
}
