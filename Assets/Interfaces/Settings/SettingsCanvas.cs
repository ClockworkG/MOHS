using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsCanvas : MonoBehaviour {
    public Settings settings;
    public Text seed; 
	// Use this for initialization
	void Start () {
        settings = GameObject.Find("Settings").GetComponent<Settings>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        settings.musicSeed = seed.text;
	}
}
