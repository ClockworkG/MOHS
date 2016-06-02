using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Lobby : NetworkBehaviour {
    public MOHSNetworkManager net;
    public string scene;
    public Canvas loading;
	// Use this for initialization
	void Start () {
        loading.enabled = false;
        net = GameObject.Find("NetworkManager").GetComponent<MOHSNetworkManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void PlayNow()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        net.ServerChangeScene(scene);
    }
}
