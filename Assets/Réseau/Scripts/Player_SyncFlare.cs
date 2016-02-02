using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SyncFlare : NetworkBehaviour {

    [SerializeField]
    private GameObject m_flare;
    private GameObject current_flare;
    public int flare_number = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame1
    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.F) && flare_number > 0)
            CmdSpawnFlare();
    }

    [Command]
    void CmdSpawnFlare()
    {
        if (current_flare == null)
        {
            flare_number--;
            Vector3 spawnPos = transform.position + transform.forward;
            spawnPos.y += 0.5f;
            GameObject flare = (GameObject)Instantiate(m_flare, spawnPos, Quaternion.identity);
            current_flare = flare;
            NetworkServer.Spawn(flare);
            flare.GetComponent<Rigidbody>().AddForce(transform.forward, UnityEngine.ForceMode.Impulse);
        }
    }
}