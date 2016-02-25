using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JetPack : MonoBehaviour {
    CharacterController Charc;
    public Image img; 
	// Use this for initialization
	void Start () {
        Charc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A) && img.fillAmount>0)
        {
            Vector3 velocity = new Vector3(Charc.velocity.x / 10, 0.2f, Charc.velocity.z / 10);
            Charc.Move(velocity);
            img.fillAmount -= 0.01f;
        }
	}
}
