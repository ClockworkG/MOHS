using UnityEngine;
using System.Collections;

public class FanBoySuccess : MonoBehaviour {
    private float elapsed = 0f;
    void OnTriggerStay()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 60f)
        {
            PlayerPrefs.SetInt("Fan", 1);
            this.enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        elapsed= 0f;
    }
}
