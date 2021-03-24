﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyonImpact : MonoBehaviour
{
 
    //when colliding with a GameObject with the tag enemy this object gets distroyed 
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "enemy" && col.gameObject.layer == 11)
        {
           
            Destroy(gameObject, 0.1f);
        }
    }
}
