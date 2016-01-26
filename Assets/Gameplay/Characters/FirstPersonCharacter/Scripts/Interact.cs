using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interact : MonoBehaviour
{
    private List<GameObject> inventory;

    void Start()
    {
        inventory = new List<GameObject>();
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
                inventory.Add(other.gameObject);
                GameObject.Destroy(other.gameObject);
            }          
        }
    }    
}
