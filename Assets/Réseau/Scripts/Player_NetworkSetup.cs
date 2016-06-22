using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

public class Player_NetworkSetup : NetworkBehaviour
{
    public float lag;
    [SerializeField]
    Camera FPSCharacterCam;
    [SerializeField]
    AudioListener audioListener;
    [SerializeField]
    GameObject suit;
    public TextMesh txt;
    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            txt.text = PlayerPrefs.GetString("Name", "Manuel");
            txt.gameObject.GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj = gameObject;
            if (SceneManager.GetActiveScene().name != "Lobby")
            {
                if (SceneManager.GetActiveScene().name == "Alpha" && isServer)
                    CmdSpawnRover();
                else if (SceneManager.GetActiveScene().name == "Beta" && isServer)
                {
                    CmdSpawnMiniGame();
                    CmdSpawnValves();
                }
                    
                else if (SceneManager.GetActiveScene().name == "Gamma" && isServer)
                    CmdSpawnPanels();
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
                GetComponent<CharacterController>().enabled = true;
                GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
                FPSCharacterCam.enabled = true;
                audioListener.enabled = true;
                suit.SetActive(false);
            }
            else
            {
                if (!isServer)
                    transform.Translate(lag, 0, 0);
            }
                
        }
    }

    [Command]
    void CmdSpawnScreen()
    {
        Transform anchor = GameObject.Find("Anchor").transform;
        GameObject scr = (GameObject)Instantiate(GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs[5], anchor.position, anchor.rotation);
        NetworkServer.Spawn(scr);
    }

    [Command]
    void CmdSpawnRover()
    {
        GameObject rov = (GameObject)Instantiate(GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs[1]);
        NetworkServer.Spawn(rov);
    }

    [Command]
    void CmdSpawnMiniGame()
    {
        Transform anchor = GameObject.FindGameObjectWithTag("Anchor").transform;
        GameObject mini_game = (GameObject)Instantiate(GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs[3], anchor.position, anchor.rotation);
        NetworkServer.Spawn(mini_game);
    }

    [Command]
    void CmdSpawnValves()
    {
        GameObject spawn;
        for (int i = 1; i <= 4; i++)
        {
            spawn = GameObject.Find("Spawn" + i.ToString());
            GameObject valve = (GameObject)Instantiate(GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs[4], spawn.transform.position, spawn.transform.rotation);
            NetworkServer.Spawn(valve);
        }
    }

    [Command]
    void CmdSpawnPanels()
    {
        GameObject empty;
        for (int i = 1; i <= 4; i++)
        {
            empty = GameObject.Find("Spawn" + i.ToString());
            GameObject pan = (GameObject)Instantiate(GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs[2], empty.transform.position, empty.transform.rotation);
            switch (i)
            {
                case 1:
                    pan.GetComponent<SolarRotation>().Direction[0] = 23;
                    pan.GetComponent<SolarRotation>().Direction[1] = 2;
                    pan.GetComponent<SolarRotation>().Direction[2] = 19;
                    pan.GetComponent<SolarRotation>().Direction[3] = 3;
                    break;
                case 2:
                    pan.GetComponent<SolarRotation>().Direction[0] = 3;
                    pan.GetComponent<SolarRotation>().Direction[1] = 7;
                    pan.GetComponent<SolarRotation>().Direction[2] = 5;
                    pan.GetComponent<SolarRotation>().Direction[3] = 1;
                    break;
                case 3:
                    pan.GetComponent<SolarRotation>().Direction[0] = 2;
                    pan.GetComponent<SolarRotation>().Direction[1] = 3;
                    pan.GetComponent<SolarRotation>().Direction[2] = 17;
                    pan.GetComponent<SolarRotation>().Direction[3] = 1;
                    break;
                case 4:
                    pan.GetComponent<SolarRotation>().Direction[0] = 2;
                    pan.GetComponent<SolarRotation>().Direction[1] = 7;
                    pan.GetComponent<SolarRotation>().Direction[2] = 13;
                    pan.GetComponent<SolarRotation>().Direction[3] = 5;
                    break;
            }
            NetworkServer.Spawn(pan);
        }
    }
}
