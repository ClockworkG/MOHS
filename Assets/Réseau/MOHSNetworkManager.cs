using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;



public class MOHSNetworkManager : NetworkManager {
    private int playtime;
    private int seconds;
    private int minutes;
    private int hours;

    public int Seconds
    {
        get { return seconds; }
    }

    public int Minutes
    {
        get { return minutes; }
    }

    public int Hours
    {
        get { return hours; }
    }

    void Start()
    {
        playtime = PlayerPrefs.GetInt("Playtime");
        StartCoroutine("Playtimer");
    }

    private IEnumerator Playtimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            playtime += 1;
            seconds = (playtime % 60);
            minutes = (playtime / 60) % 60;
            hours = (playtime / 3600) % 24;
            PlayerPrefs.SetInt("Playtime", playtime);
        }
    }

    public void Success()
    {
        GameObject.Find("SuccessCanvas").GetComponent<Canvas>().enabled = true;
    }

    public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/clockworkgames1/");
    }

    public void Website()
    {
        Application.OpenURL("http://clockworkg.github.io/");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void JoinGame()
    {
        SetIPAdress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAdress()
    {
        string ipAdress = GameObject.Find("InputFieldIPAdress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAdress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
            SetupMenuSceneButtons();
    }

    void SetupMenuSceneButtons()
    {
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);
    }
}
