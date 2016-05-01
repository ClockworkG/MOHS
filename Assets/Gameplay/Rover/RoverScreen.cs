using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RoverScreen : NetworkBehaviour {
    public RoverDisplace rv_disp;
    public SpriteRenderer sr;
    public PlayerSync player_sync;
    public float speed_tr;
    public float speed_rt;
    // Use this for initialization
    void Start () {
        rv_disp.enabled = false;
	}

	
	// Update is called once per frame
	void FixedUpdate () {
	    if (player_sync != null && player_sync.isLocalPlayer && rv_disp.battery)
        {
            if (player_sync.isServer)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                    rv_disp.Translate(0, -speed_tr, 0);
                if (Input.GetKey(KeyCode.DownArrow))
                    rv_disp.Translate(0, speed_tr, 0);
                if (Input.GetKey(KeyCode.LeftArrow))
                    rv_disp.Rotate(-speed_rt, 0, 0);
                if (Input.GetKey(KeyCode.RightArrow))
                    rv_disp.Rotate(speed_rt, 0, 0);
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    player_sync.CmdSyncRoverPos(0, -speed_tr, 0);
                    rv_disp.Translate(0, -speed_tr, 0);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    player_sync.CmdSyncRoverPos(0, speed_tr, 0);
                    rv_disp.Translate(0, speed_tr, 0);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    player_sync.CmdSyncRoverRot(-speed_rt, 0, 0);
                    rv_disp.Rotate(-speed_rt, 0, 0);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    player_sync.CmdSyncRoverRot(speed_rt, 0, 0);
                    rv_disp.Rotate(speed_rt, 0, 0);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (rv_disp.battery)
            sr.enabled = false;
        player_sync = other.gameObject.GetComponent<PlayerSync>();
    }

    void OnTriggerExit(Collider other)
    {
        player_sync = null;
    }
}
