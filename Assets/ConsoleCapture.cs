using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ConsoleCapture : MonoBehaviour {

    private FirstPersonController fps_controller;
    private GameObject com;
    public MeshRenderer Txt;
    private bool capt = false;

    void Start()
    {
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
