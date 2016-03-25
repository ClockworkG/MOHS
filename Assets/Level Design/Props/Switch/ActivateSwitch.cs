using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ActivateSwitch : NetworkBehaviour {
    private bool moving = false;
    private float displace = 0;
    private bool activated = false;
    public List<Light> lights_to_switch;
    public HorizontalAnim door;
    public MeshRenderer text_mesh;

    void Start()
    {
    }

    void Update()
    {
        if (moving)
            Activate();
    }

    [ClientRpc]
    void RpcSend()
    {
        for (int i = 0; i < lights_to_switch.Count; i++)
            lights_to_switch[i].enabled = true;
        door.Locked = false;
    }

    void Activate()
    {
        if (displace < 180)
        {
            transform.Rotate(-2, 0, 0);
            displace += 2;
        }
        else
        {
            moving = false;
            activated = true;
            door.Locked = false;
            for (int i = 0; i < lights_to_switch.Count; i++)
                lights_to_switch[i].enabled = true;
            if (isServer)
                RpcSend();
            else
                GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>().CmdSync1();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        text_mesh.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        //text_mesh.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            moving = true;
            text_mesh.enabled = false;
        }    
    }
}
