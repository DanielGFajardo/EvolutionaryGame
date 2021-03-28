using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class icons : MonoBehaviour

{

    [SerializeField] public RawImage icon;
    private Transform thing;

    // Start is called before the first frame update
    void Start()
    {
        thing = GetComponent<Transform>();
        
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
