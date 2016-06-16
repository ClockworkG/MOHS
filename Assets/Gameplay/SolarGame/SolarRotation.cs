using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class SolarRotation : NetworkBehaviour
{
    public AudioSource aud;
    public PlayerSync sync;
    public Text txt;
    public int[] Direction = new int[4];
    public int CDirection = 1;
    public Transform Solar;
    public int rotating = 0;
    public float delta = 0f;
    public float rotationspeed = 0f;
    static public int num;
    public int m_num;

    void Start()
    {
        num++;
        m_num = num;
        switch (m_num)
        {
            case 1:
                Direction[0] = 23;
                Direction[1] = 2;
                Direction[2] = 19;
                Direction[3] = 3;
                break;
            case 2:
                Direction[0] = 3;
                Direction[1] = 7;
                Direction[2] = 5;
                Direction[3] = 1;
                break;
            case 3:
                Direction[0] = 2;
                Direction[1] = 3;
                Direction[2] = 17;
                Direction[3] = 1;
                break;
            case 4:
                Direction[0] = 2;
                Direction[1] = 7;
                Direction[2] = 13;
                Direction[3] = 5;
                break;
        }
        gameObject.name = "Solar" + m_num.ToString();
        if (!isServer)
            transform.rotation = GameObject.Find("Spawn" + m_num.ToString()).transform.rotation;
        aud = gameObject.GetComponent<AudioSource>();
        txt.text = "Solar Panel Num. " + m_num.ToString() + "\n\n" + "Impossible operation : panel->display_voltage()" + "\n" + "Exception : x86475264" + "\n" + "Use arrows to rotate" + "\nCurrent rotation : " + CDirection.ToString();
    }

    [ClientRpc]
    void RpcRotPanel(int new_rot, int new_dir)
    {
        rotating = new_rot;
        CDirection = new_dir;
        if (CDirection > 3)
            CDirection = 0;
        else if (CDirection < 0)
            CDirection = 3;
    }

    void OnTriggerStay(Collider other)
    {
        if (rotating == 0 && Input.GetKey(KeyCode.LeftArrow))
        {
            aud.Play();
            if (sync.isServer)
                RpcRotPanel(2, CDirection + 1);
            else
            {
                rotating = 2;
                CDirection++;
                DirectionCamp();
                sync.CmdSyncRotPanel(m_num, CDirection, 2);
            }

            txt.text = "Solar Panel Num. " + m_num.ToString() + "\n\n" + "Impossible operation : panel->display_voltage()" + "\n" + "Exception : x86475264" + "\n" + "Use arrows to rotate" + "\nCurrent rotation : " + CDirection.ToString();
        }
        else if (rotating == 0 && Input.GetKey(KeyCode.RightArrow))
        {
            aud.Play();
            if (sync.isServer)
                RpcRotPanel(1, CDirection - 1);
            else
            {
                rotating = 1;
                CDirection--;
                DirectionCamp();
                sync.CmdSyncRotPanel(m_num, CDirection, 1);
            }
            txt.text = "Solar Panel Num. " + m_num.ToString() + "\n\n" + "Impossible operation : panel->display_voltage()" + "\n" + "Exception : x86475264" + "\n" + "Use arrows to rotate" + "\nCurrent rotation : " + CDirection.ToString();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        sync = other.gameObject.GetComponent<PlayerSync>();
    }

    void OnTriggerExit(Collider other)
    {
        sync = null;
    }

    void FixedUpdate()
    {
        txt.text = "Solar Panel Num. " + m_num.ToString() + "\n\n" + "Impossible operation : panel->display_voltage()" + "\n" + "Exception : x86475264" + "\n" + "Use arrows to rotate" + "\nCurrent rotation : " + CDirection.ToString();
        if (rotating == 1)
            RotateRight();
        else if (rotating == 2)
            RotateLeft();
    }

    void RotateRight()
    {
        Solar.Rotate(0, 0, rotationspeed);
        delta += rotationspeed;
        rotationspeed = 4.6f - Mathf.Abs(4.5f - delta / 10f);
        RotationClamp();
        if (delta >= 90)
        {
            rotating = 0;
            delta = 0;
        }
    }

    void RotateLeft()
    {
        Solar.Rotate(0, 0, -rotationspeed);
        delta += rotationspeed;
        rotationspeed = 4.6f - Mathf.Abs(4.5f - delta / 10f);
        RotationClamp();
        if (delta >= 90)
        {
            rotating = 0;
            delta = 0;
        }
    }

    void RotationClamp()
    {
        if (rotationspeed > 0.5f)
            rotationspeed = 0.5f;
    }

    void DirectionCamp()
    {
        if (CDirection > 3)
            CDirection = 0;
        else if (CDirection < 0)
            CDirection = 3;
    }
}
