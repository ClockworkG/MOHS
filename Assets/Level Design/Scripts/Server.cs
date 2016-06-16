using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {
    private float[] intensity = new float[60];
    private Light lgt;
    private int i = 0;
	void Start () {
        lgt = gameObject.GetComponent<Light>();
	    for (int i = 0; i < 60; i++)
            intensity[i] = Random.Range(0, 8);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        lgt.intensity = intensity[i];
        i = (i + 1) % 60;
	}
}
