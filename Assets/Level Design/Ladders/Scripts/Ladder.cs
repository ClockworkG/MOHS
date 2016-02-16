using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Ladder : MonoBehaviour {
    public Transform character_controller;
    public bool inside = false;
    public float height_factor = 3.2f;

    void Start()
    {

    }

    void Update()
    {
        if (inside && Input.GetKey(KeyCode.Z))
            character_controller.position += Vector3.up / height_factor;
        if (inside && Input.GetKey(KeyCode.S))
            character_controller.position += Vector3.down / height_factor;
    }

    void OnTriggerEnter(Collider other)
    {
        character_controller = other.gameObject.transform;
        inside = true;
    }

    void OnTriggerExit(Collider other)
    {
        inside = false;
    }
}
