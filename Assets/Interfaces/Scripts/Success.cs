using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Success : MonoBehaviour {
    public Text playtime;
    public MOHSNetworkManager net;
    public Text completion;
    private Image[] success;
    public Sprite locked;
    private float success_number;
    public Canvas can;
    // Use this for initialization
    void Start () {
        can.enabled = false;
        float total_completion = 0f;
        string[] levels = { "Alpha", "Beta", "Gamma" };
        success = gameObject.GetComponentsInChildren<Image>();
        for (int i = 0; i < success.Length; i++)
        {
            if (PlayerPrefs.GetInt(success[i].gameObject.name) == 0)
                success[i].sprite = locked;
            else
                success_number += 1;
        }
        foreach (string s in levels)
        {
            float max = 0f;
            float audio_docs = (float)PlayerPrefs.GetInt(s + "Docs");
            if (s == "Alpha")
                max = 3f;
            else if (s == "Beta")
                max = 4f;
            else
                max = 0f;
            if (max == 0f)
                total_completion += 1;
            else
                total_completion += ((float)(audio_docs / max));
        }
        total_completion /= (levels.Length);
        total_completion += ((float)(success_number / (float)success.Length));
        total_completion /= 2;
        total_completion *= 100;
        completion.text = ((int)total_completion).ToString();
    }
	
    public void Return()
    {
        can.enabled = false;
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            can.enabled = false;
        playtime.text = "Playtime : " + net.Hours.ToString() + "h " + net.Minutes.ToString() + "m " + net.Seconds.ToString() + "s"; 
	}
}
