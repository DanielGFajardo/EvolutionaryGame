using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class test : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
  //  public Transform[] goal;
    private int whereTo = 0;
    public Transform objectg;

    // Start is called before the first frame update
    void Start()
    {
        //find player 
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //   if (!agent.pathPending && agent.remainingDistance < 0.5f)
        // go back if no patrol locations given
        //      if (goal.Length == 0)
        //          return;

        // gost's current destination
        //   agent.destination = goal[whereTo].position;
        agent.destination = objectg.position;
        // pick next point providing continouity 
        //   whereTo = (whereTo + 1) % goal.Length;
    }
}
