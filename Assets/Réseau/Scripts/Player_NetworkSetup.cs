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
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
                GetComponent<CharacterController>().enabled = true;
                GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
                FPSCharacterCam.enabled = true;
                audioListener.enabled = true;
                if (SceneManager.GetActiveScene().name != "Snadbobox")
                    suit.SetActive(false);
            }
            else
            {
                if (!isServer)
                    transform.Translate(lag, 0, 0);
            }
                
        }
    }
}
