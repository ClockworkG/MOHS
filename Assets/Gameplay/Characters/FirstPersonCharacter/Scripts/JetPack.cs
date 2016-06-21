using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class JetPack : NetworkBehaviour {
    CharacterController Charc;
    public AudioSource aud;
    public Image img;
    public float speed;
	// Use this for initialization
	void Start () {
        Charc = GetComponent<CharacterController>();
	}
	
	void FixedUpdate () {
        if (isLocalPlayer && Input.GetKey(KeyCode.A) && img.fillAmount > 0)
        {
            aud.mute = false;
            Vector3 velocity = new Vector3(Charc.velocity.x / 100, speed, Charc.velocity.z / 100);
            Charc.Move(velocity);
            img.fillAmount -= 0.01f;
        }
        else
            aud.mute = true;
        if (Input.GetKey(KeyCode.T) && img.fillAmount <1)
        {
            img.fillAmount += 0.1f;
        }
    }
}
