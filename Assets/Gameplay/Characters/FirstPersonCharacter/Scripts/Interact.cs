using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Interact : NetworkBehaviour
{
    public List<AudioClip> usb;
    public List<QuestObject> quest;

    void Start()
    {
        usb = new List<AudioClip>();
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
                usb.Add(other.gameObject.GetComponent<AudioSource>().clip);
                GameObject.Destroy(other.gameObject);
            }       
            else if (other.gameObject.tag == "FlareBox")
            {
                transform.parent.gameObject.GetComponent<Player_SyncFlare>().flare_number += 5;
                GameObject.Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "Quest")
            {
                quest.Add(other.gameObject.GetComponent<QuestObject>());
                GameObject.Destroy(other.gameObject);
            }
        }
    }
}
