using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMapTrack2 : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    { //they can't harm in shelters
        if (col.gameObject.tag == "enemy" && col.gameObject.layer == 12)
        {
            gameObject.layer = 0;
            gameObject.SetActive(false);

        }
    }
}
