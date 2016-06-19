using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Pale : MonoBehaviour {
    public float rot_speed;
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rot_speed <= 1)
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        else
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        transform.Rotate(0, 0, rot_speed);
	}
}
