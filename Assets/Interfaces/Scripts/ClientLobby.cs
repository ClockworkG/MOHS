using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ClientLobby : MonoBehaviour {
    public string player_name;
    public InputField input;

    void Start()
    {
        if (PlayerPrefs.GetString("Name", " ") == " ")
        {
            PlayerPrefs.SetString("Name", "Manuel");
            player_name = "Manuel";
        }

        else
        {
            player_name = PlayerPrefs.GetString("Name");
            input.text = PlayerPrefs.GetString("Name");
        }
    }

    void Update()
    {
        player_name = input.text;
        PlayerPrefs.SetString("Name", player_name);
    }
}
