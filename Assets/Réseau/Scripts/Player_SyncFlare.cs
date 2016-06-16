using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_SyncFlare : NetworkBehaviour {

    [SerializeField]
    private GameObject m_flare;
    private GameObject current_flare;
    public int flare_number = 0;
    public Text flareDisplay;
    void Start()
    {
        flare_number = PlayerPrefs.GetInt("Flares");
    }

    void FixedUpdate()
    {
        PlayerPrefs.SetInt("Flares", flare_number);
        flareDisplay.text = flare_number.ToString();
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.F) && flare_number > 0)
        {
            flare_number--;
            CmdSpawnFlare();
        }
            
    }

    [Command]
    void CmdSpawnFlare()
    {
        if (current_flare == null)
        {
            PlayerPrefs.SetInt("Flares", flare_number);
            Vector3 spawnPos = transform.position + transform.forward;
            spawnPos.y += 0.5f;
            GameObject flare = (GameObject)Instantiate(m_flare, spawnPos, Quaternion.identity);
            current_flare = flare;
            NetworkServer.Spawn(flare);
            flare.GetComponent<Rigidbody>().AddForce(transform.forward, UnityEngine.ForceMode.Impulse);
        }
    }

    
}