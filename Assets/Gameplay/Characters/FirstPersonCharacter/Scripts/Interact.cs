using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Interact : NetworkBehaviour
{
    public List<AudioClip> usb;
    public List<QuestObject> quest;
    public TextMesh txt;
    public GameObject help;
    public Light spot;

    void Start()
    {
        spot = gameObject.GetComponent<Light>();
        usb = new List<AudioClip>();
        txt = help.GetComponent<TextMesh>();
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

    void FixedUpdate()
    {
        if (spot.enabled)
            txt.color = Color.black;
        else
            txt.color = Color.white;
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
                if (usb.Count > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Docs"))
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Docs", usb.Count);
            }
                
            else if (other.gameObject.tag == "FlareBox")
            {
                transform.parent.gameObject.GetComponent<Player_SyncFlare>().flare_number += 5;
                PlayerPrefs.SetInt("Flares", transform.parent.gameObject.GetComponent<Player_SyncFlare>().flare_number);
            }
            else if (other.gameObject.tag == "Quest")
                quest.Add(other.gameObject.GetComponent<QuestObject>());
            GameObject.Destroy(other.gameObject);
            help.SetActive(false);
        }
    }
}
