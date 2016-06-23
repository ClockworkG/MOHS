using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnigmaButton : NetworkBehaviour
{
    public AudioSource aud;
    public int tag_num;
    public Animation anim;
    public Transform right_pan;
    public Transform left_pan;
    public bool press = false;
    public float P = 0;
    public float lim;
    public bool close = false;
    public float speed;
    private float elapsed = 0;
    public float timeLimit;
    private PlayerSync sync;

    void FixedUpdate()
    {
        if (press && P < lim)
            Open();
        else if (P >= lim)
            press = false;
        if (P >= lim && elapsed < timeLimit)
            elapsed += Time.fixedDeltaTime;
        else if (elapsed >= timeLimit)
            close = true;
        if (close && P > 0)
            Close();
    }

    void OnTriggerEnter(Collider other)
    {
        sync = other.gameObject.GetComponent<PlayerSync>();
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && sync.isLocalPlayer)
        {
            if (elapsed == 0)
            {
                aud.Play();
                press = true;
                anim.Play();
            }
        }  
    }

    void Open()
    {
        if (isClient && !isServer)
            sync.CmdSyncDoorPos(0, 0, -speed, tag_num);
        left_pan.Translate(0, 0, -speed);
        right_pan.Translate(0, 0, speed);
        P=P+speed;
    }

    void Close()
    {
        if (isClient && !isServer)
            sync.CmdSyncDoorPos(0, 0, speed, tag_num);
        left_pan.Translate(0, 0, speed);
        right_pan.Translate(0, 0, -speed);
        P = P - speed;
        if (P <= 0)
        {
            elapsed = 0;
            close = false;
        }
    }
}

