using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour {
    public GameObject inventory_canvas;
    public GameObject audio_canvas;
    public FirstPersonController fps_controller;
    public Interact inventory_script;
    public Player_SyncFlare flare_script;
    public Text flares_display;
    public GameObject item_canvas;
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
            if (is_open)
            {
                flares_display.text = flare_script.flare_number.ToString();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            is_open = !is_open;
            audio_canvas.SetActive(is_open);
            fps_controller.enabled = !is_open;
            if (is_open)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameObject.Find("SoundGen").GetComponent<Test_Proc>().enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            is_open = !is_open;
            item_canvas.SetActive(is_open);
            fps_controller.enabled = !is_open;
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
