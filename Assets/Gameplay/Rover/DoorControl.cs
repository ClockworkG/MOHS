using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DoorControl : NetworkBehaviour {
    public Light clearLight;
    public HorizontalAnim door;
	void OnTriggerStay(Collider other)
    {
        if (other.tag == "Rover" && Input.GetKeyDown(KeyCode.E))
        {
            clearLight.color = Color.green;
            if (GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>().isServer)
                RpcDoor();
            else
                GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>().CmdSyncDoor2();
            door.locked = false;
        }
    }

    [ClientRpc]
    void RpcDoor()
    {
        door.locked = false;
    }
}
