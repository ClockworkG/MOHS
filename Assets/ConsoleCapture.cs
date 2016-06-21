using UnityEngine;
using System.Collections;

public class ConsoleCapture : MonoBehaviour {

    public MeshRenderer Txt;

    void Start()
    {
        Txt.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
            Capture(other);
    }

    void OnTriggerEnter(Collider other)
    {
        Txt.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        Txt.enabled = false;
    }

    private void Capture(Collider player)
    {
        player.attachedRigidbody.MovePosition(new Vector3(10,10, 10));
        Txt.enabled = false;
    }
}
