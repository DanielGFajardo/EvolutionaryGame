using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bossmove : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject player;
    private Animator anim;
    public Transform[] patrolpoints;

    private float distance;
    public float sighteddistance = 6f;
    public float attackrange = 3f;

    private int nextpoint = 0;

    public int maxHealth = 1000;
    public int health = 1000;
    private bool alive = true;

    public GameObject keyprefab;

    public HealthBar healthBar;
    public CharacterMove playerControl;

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<CharacterMove>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {

        if (health == 0 && alive)
        {
            StartCoroutine(Die());
        }

        if (alive)
        {

            distance = Vector3.Distance(transform.position, player.transform.position);
            //print(distance);

            if (distance > sighteddistance && !agent.pathPending && agent.remainingDistance < 0.5f) Patrol();
            if (distance < sighteddistance && distance > attackrange) follow();
            if (distance < attackrange)
            {
                attack();
            }
        }

        //if (!alive)
        //{
         //   anim.SetBool("exit", true);
        //}
    }

    private void Patrol()
    {
        // Returns if no points have been set up
        if (patrolpoints.Length == 0)
            return;

        anim.SetBool("walk", true);
        // Set the agent to go to the currently selected destination.
        agent.destination = patrolpoints[nextpoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        nextpoint = Random.Range(0,patrolpoints.Length);

    }

    private void follow()
    {
        agent.SetDestination(player.transform.position);
    }

    private void attack()
    {
        if (health > 400)
        {
            anim.SetTrigger("attack1");
            playerControl.takeDamage(10);
        }

        if (health < 401)
        {
            anim.SetTrigger("attack2");
            playerControl.takeDamage(20);
        }

    }

    IEnumerator Die()
    {
        anim.SetTrigger("dead");
        alive = false;

        yield return new WaitForSeconds(5f);

        Instantiate(keyprefab,transform.position,transform.rotation);

        Destroy(transform.gameObject);

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "puff")
        {
            health = health - 10;

            healthBar.SetHealth(health);
        }
    }

}
