using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class SplashScreen : MonoBehaviour {
    public NoiseAndGrain noise;
    public AudioSource aud;
    public Image img;
    public Image gear1;
    public Image gear2;
    public float intensity_speed;
    public float fade_speed;
    public float elapsed;
    public float speed1;
    public float speed2;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    // Update is called once per frame
    void FixedUpdate () {
        gear1.transform.Rotate(0, 0, speed1);
        gear2.transform.Rotate(0, 0, speed2);
        if (!aud.isPlaying)
        {
            if (noise.intensityMultiplier < 10f)
                noise.intensityMultiplier += intensity_speed;
            else if (img.color.a < 1f)
                img.color = new Color(1, 1, 1, img.color.a + fade_speed);
            else if (elapsed < 1f)
                elapsed += Time.fixedDeltaTime;
            else
                SceneManager.LoadScene("Menu2");
        }
        
    }
}
