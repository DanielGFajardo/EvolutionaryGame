using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logo : MonoBehaviour
{
    public Animator anim;
    int i;
    public bool now = false;
    // Start is called before the first frame update
    void Update()
    {
        i++;
        if (i == 100)
        {
            now = true;
            anim.SetTrigger("logo");
        }

    }

   
}
