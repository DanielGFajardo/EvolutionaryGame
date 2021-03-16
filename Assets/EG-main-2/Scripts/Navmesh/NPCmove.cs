using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCmove : MonoBehaviour
{

    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject player;

    private NPC npc = NPC.walk;
    public Transform[] goal;
    private int whereTo = 0;
    private Animation anim;
    private bool firedOn = false;
    public float tooClose = 60.0f;
    private Animator anima;



    // Start is called before the first frame update
    void Start()
    {

        //find player 
        player = GameObject.FindWithTag("Player");
        //get the navMesh controller
        agent = GetComponent<NavMeshAgent>();
        //get access to animation
        anim = GetComponent<Animation>();
        anima = player.GetComponentInChildren<Animator>();
        //start patrol
        walkNow();

    }

    // Update is called once per frame
    void Update()
    {

        switch (npc)
        {
            case NPC.walk:
                // when approaching the destination pick the next one
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    walkNow();
                //if close enough
                if (PlayerClose())
                    //go to attack mode
                    npc = NPC.attack;
                break;

            case NPC.attack:
                //go to move function
                moveToPlayer();

                if (firedOn == true || !PlayerClose())
                    npc = NPC.walk;
                break;


        }

    }


    private bool PlayerClose()
    {
        bool close = false;
        //check how far the player is from this demon 
        float run = Vector3.Distance(transform.position, player.transform.position);
        if (run < tooClose)
            close = true;
        else if (run > tooClose)
            close = false;
        return close;
    }
    private void walkNow()
    {
        anim.Play("idle");
            // go back if no patrol locations given
        if (goal.Length == 0)
           return;

            // gost's current destination
        agent.destination = goal[whereTo].position;
     
        // pick next point providing continouity 
        whereTo = (whereTo + 1) % goal.Length;

    }

    private void moveToPlayer()
    {   //navmesh will guide the ghost to the player
        agent.destination = player.transform.position;
        //check for distance between them
        float run = Vector3.Distance(transform.position, player.transform.position);
        // if it is less then 30f then ...
       // anim.Play("run");
        if (run < 30.0f)
        {
            //animation
            anim.Play("attack");
            anima.SetTrigger("hopp");
        }
    }
    public enum NPC { walk, attack }


    void OnCollisionEnter(Collision col)
    { // if collided with one of the beams which have puff tags coming from the unicorn's horn
        if (col.gameObject.tag == "puff")
        {
            // go to walk mode by enabling this boolean
            firedOn = true;
            //and send the ghost back to position one so its far enough from us to break the pull attraction
            transform.position = goal[0].position;
        }
    }
}
