using UnityEngine;
using System.Collections;

public class Chemical : MonoBehaviour {
    public GameObject fire1;
    public GameObject fire2;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flare")
        {
            fire1.SetActive(true);
            fire2.SetActive(true);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Flare")
        {
            if (!fire1.GetComponent<ParticleSystem>().isPlaying)
                Destroy(gameObject);
        }
    }
}
