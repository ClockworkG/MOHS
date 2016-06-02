using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Lobby : NetworkBehaviour {
    public Text selected;
    public MOHSNetworkManager net;
    public string scene;
    public Canvas loading;
    public Button[] levels;

	// Use this for initialization
	void Start () {
        Network.maxConnections = 2;
        loading.enabled = false;
        net = GameObject.Find("NetworkManager").GetComponent<MOHSNetworkManager>();
        if (PlayerPrefs.GetInt("Beta") == 1)
            levels[1].interactable = true;
        else
        {
            levels[1].interactable = false;
            levels[1].GetComponentInChildren<Text>().text = "Locked";
        }
        if (PlayerPrefs.GetInt("Gamma") == 1)
            levels[2].interactable = true;
        else
        {
            levels[2].interactable = false;
            levels[2].GetComponentInChildren<Text>().text = "Locked";
        }
            
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
        Network.maxConnections = net.numPlayers;
        net.ServerChangeScene(scene);
    }

    public void ChangeSelectedScene(string new_scene)
    {
        scene = new_scene;
        switch (scene)
        {
            case "Alpha":
                selected.text = "Awakening";
                break;
            case "Beta":
                selected.text = "Sickness";
                break;
            case "Gamma":
                selected.text = "Lapide Luteo";
                break;
        }
    }
}
