using UnityEngine;
using System.Collections;

public class LightLoader : MonoBehaviour {
    public Light[] lights;
    public Light[] corridor;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < lights.Length; i++)
            lights[i].enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < lights.Length; i++)
                lights[i].enabled = true;
            for (int i = 0; i < corridor.Length; i++)
                corridor[i].enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < lights.Length; i++)
                lights[i].enabled = false;
            for (int i = 0; i < corridor.Length; i++)
                corridor[i].enabled = true;
        }
    }
}
