using UnityEngine;
using System.Collections;

public class Stress : MonoBehaviour {
    public int stress_scale;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject.Find("SoundGen").GetComponent<Test_Proc>().stress = stress_scale;
    }
}
