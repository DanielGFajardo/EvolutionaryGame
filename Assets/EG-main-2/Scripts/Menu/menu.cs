using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class menu : MonoBehaviour
{
    public Animator anim;
    private int load;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fade(1);        
        }
    }

      
    
    public void Fade(int levelIndex)
    {
        load = levelIndex;
         anim.SetTrigger("fade");
    }

    public void NewScene()
    {
        SceneManager.LoadScene(load);
    }
}
