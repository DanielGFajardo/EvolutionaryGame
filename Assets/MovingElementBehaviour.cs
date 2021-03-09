using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingElementBehaviour : MonoBehaviour
{
       
    void Start () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = new Vector3(22.09f,transform.position.y,24.68f); 
        Debug.Log("moving");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
