using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SASLevel2 : NetworkBehaviour {
    public HorizontalAnim door1;
    public HorizontalAnim door2;
    public GameObject alarm_lights;
    public GameObject steam;
    public ParticleSystem steam_part;
    public Light alarm;
    public int number = 0;
    public int required = 0;
    public bool waterSas = false;
    public NetworkManager networkManager;
    // Use this for initialization
    void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<MOHSNetworkManager>();
        if (waterSas)
            required = 2;
        else
        {
            if (networkManager.numPlayers == 1)
                required = 2;
            else
                required = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (steam.activeInHierarchy && !steam_part.isPlaying && alarm_lights.activeInHierarchy)
        {
            door2.locked = false;
            alarm_lights.SetActive(false);
            door2.moving = true;
            alarm.color = Color.green;
            alarm.intensity = 4f;
            RpcSend();
        }
    }

    [ClientRpc]
    void RpcSend()
    {
        door2.locked = false;
        alarm_lights.SetActive(false);
        door2.moving = true;
        alarm.color = Color.green;
        alarm.intensity = 4f;
    }

    [ClientRpc]
    void RpcEvent()
    {
        door1.locked = true;
        door1.moving = true;
        alarm_lights.SetActive(true);
        steam.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!steam.activeInHierarchy)
        {
            if (other.gameObject.tag == "Player")
                number++;
            if (number == required)
            {
                door1.locked = true;
                door1.moving = true;
                alarm_lights.SetActive(true);
                steam.SetActive(true);
                RpcEvent();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!steam.activeInHierarchy)
            number--;
    }
}
