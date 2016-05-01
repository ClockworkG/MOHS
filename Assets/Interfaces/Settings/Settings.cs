using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {
    public float volumeEffects;
    public float volumeMusic;
    public float volumeVoice;
    public string musicSeed;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
