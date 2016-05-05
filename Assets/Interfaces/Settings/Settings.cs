using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {
    public float volumeEffects;
    public float volumeMusic;
    public float volumeVoice;
    public string musicSeed;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(gameObject);
    }
}
