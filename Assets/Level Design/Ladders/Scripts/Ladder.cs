using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Ladder : MonoBehaviour {
    public CharacterController character_controller;
    public bool inside = false;
    public float height_factor = 0.0001f;
    

    void FixedUpdate()
    {
        if (inside && Input.GetKey(KeyCode.Z))
            character_controller.Move(new Vector3(0, height_factor / 4, 0));
        if (inside && Input.GetKey(KeyCode.S))
            character_controller.Move(new Vector3(0, -height_factor / 4, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        character_controller = other.gameObject.GetComponent<CharacterController>();
        inside = true;
    }

    void OnTriggerExit(Collider other)
    {
        inside = false;
    }
}
