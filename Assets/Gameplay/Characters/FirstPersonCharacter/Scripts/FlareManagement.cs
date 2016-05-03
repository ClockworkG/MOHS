using UnityEngine;
using System.Collections;

public class FlareManagement : MonoBehaviour {
    private ParticleSystem m_part;
    public Light m_light;
    private Rigidbody rigid;
    private AudioSource aud;
	// Use this for initialization
	void Start () {
        aud = this.GetComponent<AudioSource>();
        aud.volume = GameObject.Find("Settings").GetComponent<Settings>().volumeEffects;
        m_part = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        m_light.intensity -= 0.0037f;
        if (!m_part.IsAlive() || !aud.isPlaying)
            Destroy(gameObject);
	}
}
