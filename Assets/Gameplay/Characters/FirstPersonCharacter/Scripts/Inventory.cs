using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour {
    public GameObject inventory_canvas;
    public FirstPersonController fps_controller;
    public Interact inventory_script;
    public Player_SyncFlare flare_script;
    private bool is_open = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.I))
        {
            is_open = !is_open;
            inventory_canvas.SetActive(is_open);
            fps_controller.enabled = !is_open;
            if (is_open)
            {
                inventory_canvas.GetComponentInChildren<Text>().text = flare_script.flare_number.ToString();
            }
        }
	}
}
