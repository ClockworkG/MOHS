using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RoverScreen : NetworkBehaviour {
    public RoverDisplace rv_disp;
    public RoverBattery rv_bat;
    public SpriteRenderer sr;
    public PlayerSync player_sync;
    public float speed_tr;
    public float speed_rt;
    // Use this for initialization
    void Start () {
        rv_disp.enabled = false;
        sr.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (player_sync != null && player_sync.isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                rv_disp.Translate(0, -speed_tr * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.DownArrow))
                rv_disp.Translate(0, speed_tr * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.LeftArrow))
                rv_disp.Rotate(-speed_rt * Time.deltaTime, 0, 0);
            if (Input.GetKey(KeyCode.RightArrow))
                rv_disp.Rotate(speed_rt * Time.deltaTime, 0, 0);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        player_sync = other.gameObject.GetComponent<PlayerSync>();
        if (rv_disp.battery)
        {
            sr.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        player_sync = null;
        rv_disp.enabled = false;
    }
}
