using UnityEngine;
using System.Collections;

public class FlashLight : MonoBehaviour {
    private Light m_light;
	// Use this for initialization
	void Start () {
        m_light = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
            m_light.enabled = !(m_light.enabled);
    }
}
