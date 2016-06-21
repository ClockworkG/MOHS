using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ConsoleCapture : MonoBehaviour {

    private FirstPersonController fps_controller;
    private GameObject com;
    public MeshRenderer Txt;

    void Start()
    {
        Txt.enabled = false;
        com = gameObject;
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)&&fps_controller.pauseEnabled)
            Capture();
        else if (Input.GetKeyDown(KeyCode.Escape))
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
        fps_controller.EnableControl();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Txt.enabled = true;
    }

    private void Capture()
    {
        Txt.enabled = false;
        fps_controller.DisableControl();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
