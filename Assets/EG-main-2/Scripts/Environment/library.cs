using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class library : MonoBehaviour
{
    public GameObject bookcase;

    //When the character reaches the "floor" after jumping can jump is set to true and the jump action is replaced by running or idle
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            bookcase.SetActive(true);
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            bookcase.SetActive(false);
        }


    }
}
