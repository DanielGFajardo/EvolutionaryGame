using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{

    public Transform Player;
    private int speed = 2;
    private bool canjump = true;
    public Rigidbody rb;
    private float smoothTime = 0.1f;
    private float turnSmooth;
    public Transform camera;
    public GameObject stroll;
    public Rigidbody firePrefab;
    public Transform fireEnd;
    private Animator anim;
    public Transform fountain;
    public Transform trailEnd;
    public Rigidbody trail1Prefab;
    public Rigidbody trail2Prefab;
    public Rigidbody trail3Prefab;
    public int maxHealth=10;
    public int health;
    public HealthBar healthBar;
    private int regenrate = 1;
    private int damagerate = 2;
    public MapVirtualization director;
    public Vector3 startPosition;

    public int keynum = 0;

    [SerializeField] public RawImage[] icons;
    public Transform door;
    public GameObject bossprefab;
    private bool bossinlevel;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = Player.position;
        health=maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody>();
        anim= gameObject.GetComponentInChildren<Animator>();
        for (int i = 0; i< icons.Length; i++)
        {
            icons[i].enabled = false;
        }
        //fountain = GameObject.Find("fountain(Clone)").transform;
    }

    //Controls the movement of the character
    void FixedUpdate()
    {
        Move();
        Jump();
        if (health < maxHealth)
        {
            //StartCoroutine(Regen());
        }
        InvokeBoss(); 
    }
    public void takeDamage(int damage){
            health = health - (damage) * damagerate;
            healthBar.SetHealth(health);
            if(health <= 0){
                print("Death");
                Player.position = startPosition;
                director.ChangeMap();
                healthBar.SetHealth(maxHealth);
                health = maxHealth;
            }   
    }
    void Move()
    {
     //get the Axes 
    float hor = Input.GetAxisRaw("Horizontal");
    float vert = Input.GetAxisRaw("Vertical");
    //normalise if moving two direction it does not go faster
    Vector3 dir = new Vector3(hor, 0f, vert).normalized;
    


        if (dir.magnitude >= 0.1f)
        {
            //atan find angle between the two axis to get to the point z comes first to get 0 on x axis (normaly it should be x and y) then clockwise motion plus add camera angle to syncronise
            float turnAngle = Mathf.Atan2(dir.z, -dir.x) * Mathf.Rad2Deg + camera.eulerAngles.y;
            //smothen turn
            float smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, turnAngle, ref turnSmooth, smoothTime);
            //rotate
            transform.rotation = Quaternion.Euler(0f, smooth, 0f);
            //to move not just point in the right direction
            Vector3 movDir = Quaternion.Euler(0f, turnAngle, 0f) * Vector3.left;
            //move character
            Player.position += movDir.normalized * speed * Time.deltaTime;
        }
        // Sound and animation for movment 
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
        //sound
        stroll.SetActive(true);
        //animation
        anim.SetTrigger("run");
        

        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            //stop sound
            stroll.SetActive(false);
            anim.SetTrigger("stop");
        }
    }
    void Jump()
    {
        //If the spacebar is pressed and the character is on the cubes with tags "floor" a force is applied on the y-axis and the animation for jump is activated
        if (Input.GetKeyDown(KeyCode.Space) && canjump)
        {
            canjump = false;
            rb.AddForce(0, 8, 0, ForceMode.Impulse);
            anim.SetTrigger("jump");
        }

    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "floor" && Input.GetKey(KeyCode.C))
        {

            anim.SetTrigger("attack");
            Rigidbody fireInstance;
            //the instance will create the prfab at the given gameObject position 
            fireInstance = Instantiate(firePrefab, fireEnd.position, fireEnd.rotation) as Rigidbody;
            //with direction and force
            fireInstance.AddForce(fireEnd.forward * 1000);
            
        }

        if (col.tag == "floor" && Input.GetKey(KeyCode.R))
        {

           
            Rigidbody trailInstance;
            //the instance will create the prfab at the given gameObject position 
            trailInstance = Instantiate(trail1Prefab, trailEnd.position, trailEnd.rotation) as Rigidbody;
            //with direction and force
            trailInstance.AddForce(fireEnd.forward * 5);

        }
        if (col.tag == "floor" && Input.GetKey(KeyCode.Q))
        {


            Rigidbody trailInstance;
            //the instance will create the prfab at the given gameObject position 
            trailInstance = Instantiate(trail2Prefab, trailEnd.position, trailEnd.rotation) as Rigidbody;
            //with direction and force
            trailInstance.AddForce(fireEnd.forward * 5);

        }
        if (col.tag == "floor" && Input.GetKey(KeyCode.T))
        {


            Rigidbody trailInstance;
            //the instance will create the prfab at the given gameObject position 
            trailInstance = Instantiate(trail3Prefab, trailEnd.position, trailEnd.rotation) as Rigidbody;
            //with direction and force
            trailInstance.AddForce(fireEnd.forward * 5);

        }

        if (col.tag == "shield")
        {
            damagerate = 1;
        }

        if (col.tag == "key")
        {
            keynum = keynum + 1;
        }

        if (col.tag == "potion")
        {
            regenrate = 2;
        }
    }

    /*
    IEnumerator Regen()
    {
        float tofountain = Vector3.Distance(fountain.position, transform.position);

        if(health < maxHealth)
        {
            if (tofountain > 5f) health = (health + 1) * regenrate;
            if (tofountain < 5f) health = (health + 5);
        }
       
    }
    */

    private void InvokeBoss()
    {
        if (keynum == 2 && !bossinlevel)
        {
            Instantiate(bossprefab,door.transform.position,door.rotation);
            bossinlevel = true;
        }
    }

    //When the character reaches the "floor" after jumping can jump is set to true and the jump action is replaced by running or idle
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "floor")
        {
            canjump = true;
        }

    }
}
