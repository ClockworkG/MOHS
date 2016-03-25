using UnityEngine;
using System.Collections;

public class Fountain : MonoBehaviour {
    private bool water = false;
    public ParticleSystem wasser;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (water)
            wasser.Emit(5);

	}
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
            water = true;
        else
            water = false;
    }
    void OnTriggerExit(Collider other)
    {
        water = false;
    }
}
