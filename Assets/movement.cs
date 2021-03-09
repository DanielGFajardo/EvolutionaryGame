using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public Transform Player;
    private float horizontal;
    private float vertcial;
    private int speed = 4;
    private Vector3 move;
    private Vector3 still = new Vector3(0, 0, 0);
    private bool canjump = true;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Controls the movement of the character
    void Update()
    {

        //The input from the arrows is stored and is used as input for a vector 
        horizontal = Input.GetAxis("Horizontal");
        vertcial = Input.GetAxis("Vertical");

        move = new Vector3(-horizontal, 0, -vertcial);
        move = move * Time.deltaTime * speed;

        Player.position += move;

        //The character is rotated taking into account the direction of movement and makes the character look in that direction
        if (move != still)
        {
            Player.rotation = Quaternion.LookRotation(move);
        }

        //If the spacebar is pressed and the character is on the cubes with tags "floor" a force is applied on the y-axis and the animation for jump is activated
        if (Input.GetKeyDown(KeyCode.Space) && canjump)
        {

            canjump = false;
            rb.AddForce(0, 8, 0, ForceMode.Impulse);

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

