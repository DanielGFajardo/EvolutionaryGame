using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bossmove : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject player;
    private Animator anim;

    private float distance;
    public float sighteddistance = 6f;
    public float attackrange = 3f;

    private NavMeshPath path;
    private bool isvalid;
    private bool walkpoint;
    private Vector3 newpos;

    public int maxHealth = 1000;
    public int health = 1000;
    private bool alive = true;

    public Rigidbody keyprefab;
    public HealthBar healthBar;
    public CharacterMove playerControl;

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<CharacterMove>();
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
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

            if (distance > sighteddistance) Patrol();
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

        if (!walkpoint)
        {
            newpos = getRandomPosition();
        }
        if (walkpoint)
        {
            agent.SetDestination(newpos);
        }

        Vector3 distanceToWalkPoint = transform.position - newpos;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkpoint = false;
            anim.SetBool("walk", false);
        }

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

    private Vector3 getRandomPosition()
    {
        Vector3 randpos = getRandomVector();

        isvalid = agent.CalculatePath(randpos, path);
        while (!isvalid)
        {
            randpos = getRandomVector();
            isvalid = agent.CalculatePath(randpos, path);
        }

        walkpoint = true;
        anim.SetBool("walk", true);
        Debug.Log(randpos);

        return randpos;
    }

    private Vector3 getRandomVector()
    {
        print("Here");
        Vector3 randpos = new Vector3 (0,0,0);

        float posx = Random.Range(-20, 20);
        float posz = Random.Range(-20, 20);

        float ax = transform.position.x;
        float az = transform.position.z;

        float newposx = ax + posx;
        float newposz = az + posz;

        if (newposx > -4f && newposx < 1f && newposz > -18f && newposz < -11f)
        {
           randpos = new Vector3(posx, 0, posz);
        }
        else
        {
            getRandomVector();
        }

        return randpos;
    }

    IEnumerator Die()
    {
        anim.SetTrigger("dead");
        alive = false;

        yield return new WaitForSeconds(5f);

        Destroy(transform.gameObject);

        Instantiate(keyprefab);
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
