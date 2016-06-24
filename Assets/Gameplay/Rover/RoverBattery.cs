using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RoverBattery : NetworkBehaviour
{
    public bool battery;
    public Quest qst;
    public RoverDisplace rv_disp;
    public Light roverLight;
    public MeshRenderer txt;
    // Use this for initialization
    void Start()
    {
        txt.enabled = false;
        if (!isServer)
            transform.rotation = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs[1].transform.rotation;
        roverLight = gameObject.GetComponent<Light>();
        if (!battery)
            roverLight.enabled = false;
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            txt.enabled = true;
            qst = other.gameObject.GetComponentInChildren<Quest>();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            ResetText();
            if (qst != null && qst.item_list.quest.Count > 0 && qst.item_list.quest[qst.index].id == "battery")
            {
                txt.enabled = false;
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
            else
                SetText();
        }
    }

    void SetText()
    {
        txt.gameObject.GetComponent<TextMesh>().color = Color.red;
        txt.gameObject.GetComponent<TextMesh>().text = "No battery equiped";
    }

    void ResetText()
    {
        txt.gameObject.GetComponent<TextMesh>().color = Color.white;
        txt.gameObject.GetComponent<TextMesh>().text = "Press 'E' to plug\n the battery";
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
        ResetText();
        txt.enabled = false;
        qst = null;
    }
}
