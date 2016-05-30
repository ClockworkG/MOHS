using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MiniGameCanvas : MonoBehaviour {
	void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            other.GetComponentInChildren<BloomOptimized>().enabled = false;
            other.transform.GetChild(2).GetComponent<Canvas>().enabled = true;
            other.transform.GetChild(2).GetComponent<MiniGame>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            other.GetComponentInChildren<BloomOptimized>().enabled = true;
            other.transform.GetChild(2).GetComponent<Canvas>().enabled = false;
            other.transform.GetChild(2).GetComponent<MiniGame>().enabled = false;
        }
    }
}
