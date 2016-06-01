using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Elevator : NetworkBehaviour
{
    public Animation anim;
    public AudioSource aud;
    public Transform mobile;
    private PlayerSync sync;
    public MeshRenderer text;
    private float delta = 0;
    public float speed;
    public float limit;
    public bool down;
    public bool up;
    private bool moving = false;
    private static int number;
    private int m_number;

    void Start()
    {
        number++;
        m_number = number;
        transform.FindChild("spikes").name = "Elevator" + m_number.ToString();
        if (up && down)
            up = false;
    }

    void MoveDown()
    {
        if (delta < limit)
        {
            mobile.Translate(0, 0, -speed);
            if (!sync.isServer && sync.isClient)
                sync.CmdMoveElevator(m_number, 0, 0, -speed, true, false);
            delta += speed;
        }
        else
        {
            delta = 0;
            RpcSyncBool(true, false);
            moving = false;
            down = true;
            up = false;
        }
    }

    [ClientRpc]
    void RpcSyncBool(bool new_down, bool new_up)
    {
        up = new_up;
        down = new_down;
    }

    void MoveUp()
    {
        if (delta < limit)
        {
            mobile.Translate(0, 0, speed);
            if (!sync.isServer && sync.isClient)
                sync.CmdMoveElevator(m_number, 0, 0, speed, false, true);
            delta += speed;
        }
        else
        {
            RpcSyncBool(false, true);
            delta = 0;
            moving = false;
            down = false;
            up = true;
        }
    }

    void FixedUpdate()
    {
        if (moving && up)
            MoveDown();
        else if (moving && down)
            MoveUp();
    }

    void OnTriggerEnter(Collider other)
    {
        text.enabled = true;
        sync = other.gameObject.GetComponent<PlayerSync>();
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && sync.isLocalPlayer && !moving)
        {
            aud.Play();
            anim.Play();
            moving = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        text.enabled = false;
    }
}
