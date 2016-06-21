using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class FlashbackCanvas : MonoBehaviour {
    public Canvas hud;
    public Canvas can;
    public AudioSource aud;
    public BloomOptimized bloom;
    public ColorCorrectionCurves sat;
    public Image img;
    public VignetteAndChromaticAberration vig;
    public bool fading = false;
    public bool fading2 = false;
    public bool playing = false;
    public bool b = false;

    void Start()
    {
        aud.volume = GameObject.Find("Settings").GetComponent<Settings>().volumeVoice;
        sat.enabled = false;
    }
	void FixedUpdate () {
	    if (fading)
        {
            if (img.color.a < 1 && !b)
                img.color = new Color(1, 1, 1, img.color.a + 0.01f);
            else if (img.color.a > 0)
            {
                b = true;
                sat.enabled = true;
                sat.saturation = 5;
                bloom.enabled = false;
                vig.enabled = true;
                vig.intensity = 0.4f;
                img.color = new Color(1, 1, 1, img.color.a - 0.01f);
            }
            else if (img.color.a <= 0)
            {
                aud.Play();
                fading = false;
                playing = true;
            }
        }
        if (playing)
        {
            if (!aud.isPlaying)
            {
                fading2 = true;
                playing = false;
                b = false;
            }
                
        }
        if (fading2)
        {
            if (img.color.a < 1 && !b)
                img.color = new Color(1, 1, 1, img.color.a + 0.01f);
            else if (img.color.a > 0)
            {
                
                b = true;
                sat.enabled = false;
                sat.saturation = 1;
                bloom.enabled = true;
                vig.enabled = false;
                vig.intensity = 0;
                img.color = new Color(1, 1, 1, img.color.a - 0.01f);
            }
            else if (img.color.a <= 0)
            {
                img.color = new Color(1, 1, 1, 0);
                fading2 = false;
                b = false;
                hud.enabled = true;
            }
        }
	}

    public void StartFlashback()
    {
        hud.enabled = false;
        can.enabled = true;
        fading = true;
    }
}
