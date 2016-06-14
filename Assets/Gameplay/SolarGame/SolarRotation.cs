using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class SolarRotation : NetworkBehaviour {
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
        gameObject.transform.parent.name = "Solar" + m_num.ToString();
        txt.text = "Solar Panel Num. " + m_num.ToString() + "\n\n" + "Impossible operation : panel->display_voltage()" + "\n" + "Exception : x86475264" + "\n" + "Use arrows to rotate" + "\nCurrent rotation : " + CDirection.ToString(); 
    }

    void OnTriggerStay(Collider other) {
        if (rotating == 0 && Input.GetKey(KeyCode.LeftArrow))
        {
            if (isServer && isClient)
                RpcSyncPanRotate(1, CDirection + 1);
            else
                GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>().CmdSyncRotPanel(m_num, CDirection + 1, 1);
            rotating = 1;
            CDirection++;
            DirectionCamp();
            txt.text = "Solar Panel Num. " + m_num.ToString() + "\n\n" + "Impossible operation : panel->display_voltage()" + "\n" + "Exception : x86475264" + "\n" + "Use arrows to rotate" + "\nCurrent rotation : " + CDirection.ToString();
        }
        else if (rotating == 0 && Input.GetKey(KeyCode.RightArrow))
        {
            if (isServer && isClient)
                RpcSyncPanRotate(2, CDirection - 1);
            else
                GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>().CmdSyncRotPanel(m_num, CDirection - 1, 2);
            rotating = 2;
            CDirection--;
            DirectionCamp();
            txt.text = "Solar Panel Num. " + m_num.ToString() + "\n\n" + "Impossible operation : panel->display_voltage()" + "\n" + "Exception : x86475264" + "\n" + "Use arrows to rotate" + "\nCurrent rotation : " + CDirection.ToString();
        }
    }

    void FixedUpdate()
    {
        if (rotating == 1)
            RotateRight();
        else if (rotating == 2)
            RotateLeft();

    }

    [ClientRpc]
    void RpcSyncPanRotate(int new_rotate, int new_direction)
    {
        rotating = new_rotate;
        CDirection = new_direction;
        txt.text = "Solar Panel Num. " + m_num.ToString() + "\n\n" + "Impossible operation : panel->display_voltage()" + "\n" + "Exception : x86475264" + "\n" + "Use arrows to rotate" + "\nCurrent rotation : " + new_direction.ToString();
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
