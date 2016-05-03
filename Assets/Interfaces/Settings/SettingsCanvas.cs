using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsCanvas : MonoBehaviour {
    public Settings settings;
    public Text seed;
    public Slider audioDocsVolume;
    public Slider soundVolume;
    public Slider musicVolume;
	// Use this for initialization
	void Start () {
        audioDocsVolume.value = PlayerPrefs.GetFloat("AudioDocs");
        soundVolume.value = PlayerPrefs.GetFloat("Effects");
        musicVolume.value = PlayerPrefs.GetFloat("Music");
        seed.text = PlayerPrefs.GetString("Seed");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        settings.musicSeed = seed.text;
        settings.volumeVoice = audioDocsVolume.value;
        settings.volumeEffects = soundVolume.value;
        settings.volumeMusic = musicVolume.value;
        PlayerPrefs.SetFloat("AudioDocs", audioDocsVolume.value);
        PlayerPrefs.SetFloat("Music", musicVolume.value);
        PlayerPrefs.SetFloat("Effects", soundVolume.value);
        PlayerPrefs.SetString("Seed", seed.text);
    }
}
