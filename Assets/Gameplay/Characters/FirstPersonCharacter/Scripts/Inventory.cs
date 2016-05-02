using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour {
    public FirstPersonController fps_controller;
    public Interact inventory_script;
    public Player_SyncFlare flare_script;
    public Text flares_display;
    public Canvas item_canvas;

	void Update () {
        if (item_canvas.enabled && (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape)))
        {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                item_canvas.enabled = false;
            
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            item_canvas.enabled = true;
        }
    }
}
