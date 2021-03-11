using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public GameObject sound;
    
    //When the character reaches the "floor" after jumping can jump is set to true and the jump action is replaced by running or idle
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            sound.SetActive(true);
        }
       

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            sound.SetActive(false);
        }


    }
}
