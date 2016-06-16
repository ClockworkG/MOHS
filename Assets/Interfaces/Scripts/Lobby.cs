using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Lobby : NetworkBehaviour {
    public Image background;
    public Text selected;
    public Text pickedUpText;
    public Text maxText;
    public MOHSNetworkManager net;
    public string scene;
    public Canvas loading;
    public Button[] levels;
    public int[] maxs;
    public int audioDocsNumber;
    public int pickedUp;
    public Sprite[] backs;
    public InputField input;
    public string player_name;

    // Use this for initialization
    void Start () {
        scene = "Alpha";
        input.text = "John Doe";
        loading.enabled = false;
        pickedUp = PlayerPrefs.GetInt(scene + "Docs");
        audioDocsNumber = maxs[0];
        pickedUpText.text = pickedUp.ToString();
        maxText.text = audioDocsNumber.ToString();
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
        if (PlayerPrefs.GetInt("Delta") == 1)
            levels[3].interactable = true;
        else
        {
            levels[3].interactable = false;
            levels[3].GetComponentInChildren<Text>().text = "Locked";
        }
        if (PlayerPrefs.GetInt("Epsilon") == 1)
            levels[4].interactable = true;
        else
        {
            levels[4].interactable = false;
            levels[4].GetComponentInChildren<Text>().text = "Locked";
        }
        if (PlayerPrefs.GetInt("Zeta") == 1)
            levels[5].interactable = true;
        else
        {
            levels[5].interactable = false;
            levels[5].GetComponentInChildren<Text>().text = "Locked";
        }
    }
	
    public void ChangeBack(int number)
    {
        background.sprite = backs[number];
    }
	// Update is called once per frame
	void Update () {
        player_name = input.text;
        if (!Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void PlayNow()
    {
        PlayerPrefs.SetString("Name", player_name);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        net.ServerChangeScene(scene);
    }

    public void ChangeSelectedScene(string new_scene)
    {
        scene = new_scene;
        pickedUp = PlayerPrefs.GetInt(scene + "Docs");
        switch (scene)
        {
            case "Alpha":
                selected.text = "Awakening";
                audioDocsNumber = maxs[0];
                break;
            case "Beta":
                selected.text = "Sickness";
                audioDocsNumber = maxs[1];
                break;
            case "Gamma":
                selected.text = "Lapide Luteo";
                audioDocsNumber = maxs[2];
                break;
            case "Delta":
                selected.text = "Error 404";
                audioDocsNumber = maxs[3];
                break;
            case "Epsilon":
                selected.text = "Hard Hat Area";
                audioDocsNumber = maxs[4];
                break;
            case "Zeta":
                selected.text = "Broadcast";
                audioDocsNumber = maxs[5];
                break;
        }
        pickedUpText.text = pickedUp.ToString();
        maxText.text = audioDocsNumber.ToString();
    }
}
