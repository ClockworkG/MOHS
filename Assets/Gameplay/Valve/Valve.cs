using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Valve : NetworkBehaviour {
    public AudioSource aud;
    public Transform transf;
    private float delta;
    public float limit;
    public float speed;
    public bool rot;
    public bool done;
    public MeshRenderer txt;
    private PlayerSync sync;
    private static int number;
    private int m_number;

    void Start()
    {
        number++;
        m_number = number;
        gameObject.name = "Valve" + m_number.ToString();
        transform.rotation = GameObject.Find("Spawn" + m_number.ToString()).transform.rotation;
        done = false;
    }

    void FixedUpdate()
    {
        if (rot)
        {
            if (delta < limit)
            {
                transf.Rotate(0, 0, speed);
                delta += speed;
            }
            else
            {
                if (sync.isServer)
                    RpcSyncRot(false);    
                else
                    sync.CmdSyncValveRot(m_number, false);
                    
                rot = false;
                done = true;
            }
                
        }
    }

    [ClientRpc]
    void RpcSyncDone()
    {
        done = true;
    }

    [ClientRpc]
    void RpcSyncRot(bool new_value)
    {
        rot = new_value;
    }

    void OnTriggerEnter(Collider other)
    {
        sync = other.GetComponent<PlayerSync>();
        if (!done)
            txt.enabled = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (!rot && !done && Input.GetKeyDown(KeyCode.E) && sync.isLocalPlayer)
        {
            if (sync.isServer)
            {
                RpcSyncDone();
                RpcSyncRot(true);
            }
                
            else
            {
                sync.CmdSyncValveRot(m_number, true);
                sync.CmdSyncValveDone(m_number);
            }
                
            aud.Play();
            rot = true;
            txt.enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        txt.enabled = false;
    }
}
