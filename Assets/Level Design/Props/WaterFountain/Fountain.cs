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
	void FixedUpdate ()
    {
        if (water)
        {
            wasser.Emit(10);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        audioSource.enabled = true;
        
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            water = true;
            audioSource.Play();
        }
        else
        {
            water = false;
            audioSource.Stop();
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        water = false;
        audioSource.enabled = false;
    }
}
