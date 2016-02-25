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
	void Start () {
        aud = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateName();
        elapsed.text = aud.time.ToString();
	}

    public void UpdateName()
    {
        title.text = audio_list.usb[index].name;
        duration.text = audio_list.usb[index].length.ToString();
        elapsed.text = "0";
    }

    public void Play()
    {
        aud.clip = audio_list.usb[index];
        GameObject.Find("SoundGen").GetComponent<Test_Proc>().enabled = false;
        aud.Play();
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
            if (t > 0)
            {
                index = (index + 1) % t;
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

            if (t > 0)
            {
                index = ((index - 1) % t);
                UpdateName();
            }
            else
                title.text = "No audio documents";
        }
    }

    public void Pause()
    {
        if (aud.isPlaying)
        {
            GameObject.Find("SoundGen").GetComponent<Test_Proc>().enabled = true;
            aud.Pause();
        }
    }

    public void Unpause()
    {
        if (!aud.isPlaying)
        {
            GameObject.Find("SoundGen").GetComponent<Test_Proc>().enabled = false;
            aud.UnPause();
        }
    }
}
