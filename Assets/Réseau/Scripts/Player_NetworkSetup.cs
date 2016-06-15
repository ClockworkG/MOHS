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
    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj = gameObject;
            if (SceneManager.GetActiveScene().name != "Lobby")
            {
                if (SceneManager.GetActiveScene().name == "Alpha" && isServer)
                    CmdSpawnRover();
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
    void CmdSpawnRover()
    {
        GameObject rov = (GameObject)Instantiate(GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs[1]);
        NetworkServer.Spawn(rov);
    }

    [Command]
    void CmdSpawnPanels()
    {
        GameObject empty;
        for (int i = 1; i <= 4; i++)
        {
            empty = GameObject.Find("Spawn" + i.ToString());
            GameObject pan = (GameObject)Instantiate(GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs[2], empty.transform.position, empty.transform.rotation);
            NetworkServer.Spawn(pan);
        }
    }
}
