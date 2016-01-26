using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_NetworkSetup : NetworkBehaviour
{

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
            GameObject.Find("Scene Camera").SetActive(false);
            GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj = this.gameObject;
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            FPSCharacterCam.enabled = true;
            audioListener.enabled = true;
            suit.SetActive(false);
        }
    }
}
