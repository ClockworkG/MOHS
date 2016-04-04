using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Interact : NetworkBehaviour
{
    public List<AudioClip> usb;
    public List<QuestObject> quest;
    public GameObject help;

    void Start()
    {
        usb = new List<AudioClip>();
    }

    void Update()
    {
    }

    public bool isInInventory(string id)
    {
        for (int i = 0; i < quest.Count; i++)
        {
            if (quest[i].id == id)
                return true;
        }
        return false;
    }

    public void DeleteItem(string id)
    {
        for (int i = 0; i < quest.Count; i++)
        {
            if (quest[i].id == id)
                quest.RemoveAt(i);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        help.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        help.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (other.gameObject.tag == "USB")
            {
                usb.Add(other.gameObject.GetComponent<AudioSource>().clip);
                help.SetActive(false);
                GameObject.Destroy(other.gameObject);
            }       
            else if (other.gameObject.tag == "FlareBox")
            {
                transform.parent.gameObject.GetComponent<Player_SyncFlare>().flare_number += 5;
                help.SetActive(false);
                GameObject.Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "Quest")
            {
                quest.Add(other.gameObject.GetComponent<QuestObject>());
                help.SetActive(false);
                GameObject.Destroy(other.gameObject);
            }
        }
    }
}
