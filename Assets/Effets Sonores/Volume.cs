using UnityEngine;
using System.Collections;

public class Volume : MonoBehaviour {
    public AudioSource aud;
	void Start () {
        aud.volume = GameObject.Find("Settings").GetComponent<Settings>().volumeEffects;
	}
}
