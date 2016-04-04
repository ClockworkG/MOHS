using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RoverBattery : NetworkBehaviour {
    public bool battery;
    public Quest qst;
    public Canvas canvas;
    public RoverDisplace rv_disp;
    public Light roverLight;
	// Use this for initialization
	void Start () {
        roverLight = gameObject.GetComponent<Light>();
        if (!battery)
        {
            roverLight.enabled = false;
            canvas.enabled = true;
        }
            
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        qst = other.gameObject.GetComponentInChildren<Quest>();
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (qst.item_list.quest.Count > 0 && qst.item_list.quest[qst.index].id == "battery")
            {
                PlayerSync player = GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>();
                if (isServer)
                    RpcSyncRover();
                else
                    player.CmdSyncRover();
                canvas.enabled = false;
                roverLight.enabled = true;
                qst.item_list.DeleteItem("battery");
                qst.index = 0;
                qst.UpdateName();
                rv_disp.battery = true;
                this.enabled = false;
            }
        }
    }

    [ClientRpc]
    public void RpcSyncRover()
    {
        canvas.enabled = false;
        roverLight.enabled = true;
        qst.item_list.DeleteItem("battery");
        qst.index = 0;
        qst.UpdateName();
        rv_disp.battery = true;
    }

    void OnTriggerExit(Collider other)
    {
        qst = null;
    }
}
