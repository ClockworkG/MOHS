using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour {
    [Command]
	public void CmdSync1()
    {
        GameObject[] lights = GameObject.FindGameObjectsWithTag("SyncLight1");
        GameObject door = GameObject.FindGameObjectWithTag("SyncDoor1");
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponentInChildren<Light>().enabled = true;
        }
        door.GetComponentInChildren<HorizontalAnim>().locked = false;
    }

    [Command]
    public void CmdChangeScene(string scene)
    {
        GameObject.Find("NetworkManager").GetComponent<NetworkManager>().ServerChangeScene(scene);
    }

    [Command]
    public void CmdSyncRover()
    {
        GameObject rover = GameObject.FindGameObjectWithTag("Rover");

        rover.GetComponentInChildren<Light>().enabled = true;
        rover.GetComponent<RoverDisplace>().battery = true;
    }
}
