using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ClientLobby : MonoBehaviour {
    public string player_name;
    public InputField input;

    void Start()
    {
        input.text = PlayerPrefs.GetString("Name", "Manuel");
    }

    void Update()
    {
        player_name = input.text;
        PlayerPrefs.SetString("Name", player_name);
    }
}
