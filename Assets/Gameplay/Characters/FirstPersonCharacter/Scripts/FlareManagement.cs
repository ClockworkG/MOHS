using UnityEngine;
using System.Collections;

public class FlareManagement : MonoBehaviour {
    private ParticleSystem m_part;
    private Light m_light;
    private Rigidbody rigid;
	// Use this for initialization
	void Start () {
        m_part = this.GetComponent<ParticleSystem>();
        m_light = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {

        m_light.intensity -= 0.0037f;
        if (!m_part.IsAlive())
            Destroy(gameObject);
	}
}
