using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RoverBattery : NetworkBehaviour
{
    public bool battery;
    public Quest qst;
    public RoverDisplace rv_disp;
    public Light roverLight;
    // Use this for initialization
    void Start()
    {
        if (!isServer)
            transform.rotation = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs[1].transform.rotation;
        roverLight = gameObject.GetComponent<Light>();
        if (!battery)
            roverLight.enabled = false;
    }



    void OnTriggerEnter(Collider other)
    {
        qst = other.gameObject.GetComponentInChildren<Quest>();
    }

    void OnTriggerStay(Collider other)
    {
        
        if (Input.GetKeyDown(KeyCode.E) && qst != null && qst.item_list.quest.Count > 0 && qst.item_list.quest[qst.index].id == "battery")
        {
            PlayerSync player = GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>();
            if (player.isServer)
                RpcSyncRover();
            else
                player.CmdSyncRover();
            roverLight.enabled = true;
            qst.item_list.DeleteItem("battery");
            qst.index = 0;
            qst.UpdateName();
            rv_disp.battery = true;
        }
    }

    [ClientRpc]
    public void RpcSyncRover()
    {
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
