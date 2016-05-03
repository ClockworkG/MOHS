using UnityEngine;
using System.Collections;

public class MenuMusic : MonoBehaviour {
    public AudioSource aud;
    public Settings settings;
	void FixedUpdate () {
        aud.volume = settings.volumeMusic;
	}
}
