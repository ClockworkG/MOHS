using UnityEngine;
using System.Collections;

public class Fountain : MonoBehaviour {
    private bool water = false;
    public ParticleSystem wasser;
    public AudioSource audioSource;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (water)
        {
            wasser.Emit(10);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            water = true;
        }
        else
        {
            water = false;
        }
        audioSource.enabled = water;
    }
    void OnTriggerExit(Collider other)
    {
        audioSource.Stop();
    }
}
