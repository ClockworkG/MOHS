using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class Vignette : MonoBehaviour {
    public float speed;
    public VignetteAndChromaticAberration vig;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (vig.intensity > 0)
            vig.intensity -= speed * Time.deltaTime;
	}
}
