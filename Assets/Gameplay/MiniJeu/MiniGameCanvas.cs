using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MiniGameCanvas : MonoBehaviour {
    public Canvas can;
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
