using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingElementBehaviour : MonoBehaviour
{
    int intDest = 0;
    new Vector3[] destinations; 
    NavMeshAgent agent;   
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        destinations = new [] {new Vector3(22.09f,transform.position.y,24.68f),new Vector3(8.519f,transform.position.y,39.48f),new Vector3(41.22f,transform.position.y,24.49f),new Vector3(7.69f,transform.position.y,10.48f)};     
        agent.destination = destinations[intDest]; 
        Debug.Log("moving");

    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    nextDestination();
                }
            }
        }
    }

    void nextDestination(){
        intDest = (intDest + 1)%destinations.Length;
        agent.destination =  destinations[intDest];
    }
}
