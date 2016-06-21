using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Timer : NetworkBehaviour {
    public Text txt;
    public Canvas can;
    public int total_time;
    public int seconds;
    public int minutes;
    void Start () {
        if (GameObject.Find("NetworkManager").GetComponent<MOHSNetworkManager>().numPlayers == 1)
            total_time *= 2;
        can.worldCamera = GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponentInChildren<Camera>();
        can.planeDistance = 0.2f;
        seconds = (total_time % 60);
        minutes = (total_time / 60) % 60;
        StartCoroutine("Time");
	}
	
	// Update is called once per frame
	void Update () {
        if (minutes == 0)
        {
            if (seconds % 2 == 0)
                txt.color = Color.red;
            else
                txt.color = Color.white;
        }
        if (seconds < 10)
            txt.text = "0" + minutes.ToString() + ":0" + seconds.ToString();
        else
            txt.text = "0" + minutes.ToString() + ":" + seconds.ToString();
        if (seconds == 0 && minutes == 0)
        {
            if (isServer)
                GameObject.Find("NetworkManager").GetComponent<NetworkManager>().ServerChangeScene(SceneManager.GetActiveScene().name);
            else
                GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>().CmdChangeScene(SceneManager.GetActiveScene().name);
        }
            
    }

    private IEnumerator Time()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            total_time -= 1;
            seconds = (total_time % 60);
            minutes = (total_time / 60) % 60;
        }
    }
}
