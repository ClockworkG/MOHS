using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MiniGameCanvas : NetworkBehaviour {
    public Canvas can;

    void Start()
    {
        transform.rotation = GameObject.FindGameObjectWithTag("Anchor").transform.rotation;
    }

	void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player" && other.gameObject.GetComponent<PlayerSync>().isLocalPlayer)
       {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            can.worldCamera = other.gameObject.GetComponentInChildren<Camera>();
       }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<PlayerSync>().isLocalPlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            can.worldCamera = null;
        }
    }
}
