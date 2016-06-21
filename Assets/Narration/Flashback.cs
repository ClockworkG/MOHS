using UnityEngine;
using System.Collections;

public class Flashback : MonoBehaviour {
    public AudioClip clip;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerSync>().isLocalPlayer)
        {
            other.gameObject.GetComponentInChildren<FlashbackCanvas>().aud.clip = clip;
            other.gameObject.GetComponentInChildren<FlashbackCanvas>().StartFlashback();
            Destroy(gameObject);
        }
    }
}
