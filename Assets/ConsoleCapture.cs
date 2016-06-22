using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ConsoleCapture : NetworkBehaviour {

    private static int number;
    private int m_num;
    private FirstPersonController fps_controller;
    private GameObject com;
    public MeshRenderer Txt;
    private bool capt = false;

    void Start()
    {
        number++;
        m_num = number;
        gameObject.name = m_num.ToString();
        if (!GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>().isServer)
        {
            Transform spawn = GameObject.Find("Spawn" + m_num.ToString()).transform;
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
        }
        Txt.enabled = false;
        com = gameObject;
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)&& fps_controller.pauseEnabled && !capt)
            Capture();
        if (Input.GetKeyUp(KeyCode.Escape) && capt)
            Release();
    }

    void OnTriggerEnter(Collider other)
    {
        fps_controller = other.gameObject.GetComponent<FirstPersonController>();
        Txt.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        fps_controller = null;
        Txt.enabled = false;
    }

    private void Release()
    {
        capt = false;
        for (int i = 0; i < 30; i++)
            fps_controller.gameObject.GetComponentInChildren<Camera>().fieldOfView += 1;
        fps_controller.EnableControl();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        fps_controller.GetComponentInChildren<Zoom>().enabled = true;
        Txt.enabled = true;
    }

    private void Capture()
    {
        capt = true;
        fps_controller.GetComponentInChildren<Zoom>().enabled = false;
        for (int i = 0; i < 30; i++)
            fps_controller.gameObject.GetComponentInChildren<Camera>().fieldOfView -= 1;
        Txt.enabled = false;
        fps_controller.DisableControl();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
