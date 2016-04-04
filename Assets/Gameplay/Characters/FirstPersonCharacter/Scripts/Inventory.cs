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
    private bool is_open = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            is_open = !is_open;
            item_canvas.enabled = is_open;
            //fps_controller.enabled = !is_open;
            if (is_open)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
