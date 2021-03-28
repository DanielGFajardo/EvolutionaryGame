using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtplayer : MonoBehaviour
{
    public CharacterMove playerControl;
    private GameObject player;
    private bossmove boss;
    private Transform weapon;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = player.GetComponent<CharacterMove>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, weapon.position);
        Debug.Log(distance);

        if (distance < 10f)
        {
            if (boss.health > 400) playerControl.takeDamage(5);
            if (boss.health < 401) playerControl.takeDamage(10);
        }
    }
}
