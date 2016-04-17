using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RoverDisplace : MonoBehaviour {
    
    public bool battery;
    public PlayerSync player;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void Translate(float x, float y, float z)
    {
        transform.Translate(x, y, z);
    }

    public void Rotate(float x, float y, float z)
    {
        transform.Rotate(x, y, z);
    }
}
