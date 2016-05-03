using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioDocs : MonoBehaviour {
    public AudioSource aud;
    public Interact audio_list;
    public int index;
    public Text title;
    public Text elapsed;
    public Text duration;
	// Use this for initialization
	void Start()
    {
        index = 0;
        aud.volume = GameObject.Find("Settings").GetComponent<Settings>().volumeVoice;
    }
	// Update is called once per frame
	void Update () {
        UpdateName();
        elapsed.text = aud.time.ToString();
	}

    public void UpdateName()
    {
        if (audio_list.usb.Count != 0)
        {
            aud.clip = audio_list.usb[index];
            title.text = audio_list.usb[index].name;
            duration.text = audio_list.usb[index].length.ToString();
            elapsed.text = "0";
        }
    }

    public void Stop()
    {
        aud.Stop();
        GameObject.Find("SoundGen").GetComponent<Test_Proc>().enabled = true;
    }

    public void Next()
    {
        if (!aud.isPlaying)
        {
            int t = audio_list.usb.Count;
            if (index <= t - 1)
            {
                if (index == t - 1)
                    index = 0;
                else
                    index++;
                UpdateName();
            }
            else
                title.text = "No audio documents";
        }
    }

    public void Prev()
    {
        if (!aud.isPlaying)
        {
            int t = audio_list.usb.Count;

            if (index >= 0)
            {
                if (index == 0)
                    index = t - 1;
                else
                    index--;
                UpdateName();
            }
            else
                title.text = "No audio documents";
        }
    }
    

    public void PlayPause()
    {
        if (!aud.isPlaying && audio_list.usb.Count!=0)
        {
            GameObject.Find("SoundGen").GetComponent<Test_Proc>().enabled = false;
            if (aud.time == 0)
                aud.Play();
            else
                aud.UnPause();
        }
        else if(aud.isPlaying)
        {
            GameObject.Find("SoundGen").GetComponent<Test_Proc>().enabled = true;
            aud.Pause();
        }
    }
}
