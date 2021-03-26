using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class icons : MonoBehaviour

{

    [SerializeField] public RawImage icon;
    private Transform thing;
    private MeshRenderer mr;
    private BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        thing = GetComponent<Transform>();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        icon.enabled = false;
        mr.enabled = false;
        bc.enabled = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            icon.enabled = true;
            Destroy(thing.gameObject);
        }
    }
}
