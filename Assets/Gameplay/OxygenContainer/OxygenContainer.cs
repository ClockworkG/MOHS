using UnityEngine;
using System.Collections;

public class OxygenContainer : MonoBehaviour {
    private JetPack jet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        jet = other.gameObject.GetComponent<JetPack>();
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (jet.img.fillAmount < 1.0f)
                jet.img.fillAmount += 0.01f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        jet = null;
    }
}
