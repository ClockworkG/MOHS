using UnityEngine;
using System.Collections;

public class RoverSuccess : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rover")
            PlayerPrefs.SetInt("Rover", 1);
    }
}
