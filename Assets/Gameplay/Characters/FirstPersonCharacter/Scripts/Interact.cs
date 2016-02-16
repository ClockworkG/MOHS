using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interact : MonoBehaviour
{
    private List<AudioSource> usb;

    void Start()
    {
        usb = new List<AudioSource>();
    }

    void Update()
    {
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (other.gameObject.tag == "USB")
            {
                usb.Add(other.gameObject.GetComponent<AudioSource>());
                GameObject.Destroy(other.gameObject);
            }       
            else if (other.gameObject.tag == "FlareBox")
            {
                transform.parent.gameObject.GetComponent<Player_SyncFlare>().flare_number += 5;
                GameObject.Destroy(other.gameObject);
            }
        }
    }    
}
