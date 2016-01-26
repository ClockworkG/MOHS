using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SyncFlare : NetworkBehaviour {

    [SerializeField]
    private GameObject m_flare;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame1
    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.F))
            CmdSpawnFlare();
    }

    [Command]
    void CmdSpawnFlare()
    {
        Vector3 spawnPos = transform.position + transform.forward;
        spawnPos.y += 0.5f;
        GameObject flare = (GameObject)Instantiate(m_flare, spawnPos, Quaternion.identity);
        //flare.GetComponent<Rigidbody>().AddTorque(Vector3.forward, UnityEngine.ForceMode.Impulse);
        //flare.GetComponent<Rigidbody>().AddTorque(Vector3.back, UnityEngine.ForceMode.Impulse);
        flare.GetComponent<Rigidbody>().AddForce(transform.forward, UnityEngine.ForceMode.Impulse);
        NetworkServer.Spawn(flare);
    }
}